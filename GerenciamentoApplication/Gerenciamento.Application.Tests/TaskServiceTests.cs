using AutoFixture;
using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Moq;
using Xunit;

namespace Gerenciamento.Application.Tests
{
    public class TaskServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDbTaskReadAdapter> _mockDbTaskReadAdapter;
        private readonly Mock<IDbTaskWriteAdapter> _mockDbTaskWriteAdapter;
        private readonly Mock<ILogUpdateAdapter> _mockLogUpdateAdapter;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _mockDbTaskReadAdapter = new Mock<IDbTaskReadAdapter>();
            _mockDbTaskWriteAdapter = new Mock<IDbTaskWriteAdapter>();
            _mockLogUpdateAdapter = new Mock<ILogUpdateAdapter>();
            _taskService = new TaskService(
                _mockDbTaskReadAdapter.Object,
                _mockDbTaskWriteAdapter.Object,
                _mockLogUpdateAdapter.Object
            );
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenDbTaskReadAdapterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TaskService(null, _mockDbTaskWriteAdapter.Object, _mockLogUpdateAdapter.Object));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenDbTaskWriteAdapterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TaskService(_mockDbTaskReadAdapter.Object, null, _mockLogUpdateAdapter.Object));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenLogUpdateAdapterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TaskService(_mockDbTaskReadAdapter.Object, _mockDbTaskWriteAdapter.Object, null));
        }

        [Fact]
        public async Task DeleteTaskAsync_DeletesTaskAndSavesLog()
        {
            // Arrange
            var task = _fixture.Create<TaskProject>();
            var user = _fixture.Create<User>();

            _mockDbTaskReadAdapter.Setup(x => x.GetByIdAsync(task.Id)).ReturnsAsync(task);

            // Act
            await _taskService.DeleteTaskAsync(task.Id, user);

            // Assert
            _mockDbTaskWriteAdapter.Verify(x => x.DeleteAsync(task), Times.Once);
            _mockLogUpdateAdapter.Verify(x => x.SaveAsync(It.Is<HistoryUpdate>(h => h.Content.Contains(task.Id.ToString()))), Times.Once);
        }

        [Fact]
        public async Task GetTaskAsync_ReturnsTasks()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var tasks = _fixture.Create<IEnumerable<TaskProject>>();

            _mockDbTaskReadAdapter.Setup(x => x.GetAllTaskByProjectAsync(projectId)).ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetTaskAsync(projectId);

            // Assert
            Assert.Equal(tasks, result);
        }

        [Fact]
        public async Task SaveTaskAsync_SavesTaskAndSavesLog_WhenTaskIsValid()
        {
            // Arrange
            var task = _fixture.Create<TaskProject>();
            var user = _fixture.Create<User>();

            _mockDbTaskReadAdapter.Setup(x => x.GetCountTaskByIdProjectAsync(task.ProjectId)).ReturnsAsync(0);
            _mockDbTaskWriteAdapter.Setup(x => x.SaveAsync(task)).ReturnsAsync(task.Id);

            // Act
            await _taskService.SaveTaskAsync(task, user);

            // Assert
            _mockDbTaskWriteAdapter.Verify(x => x.SaveAsync(task), Times.Once);
            _mockLogUpdateAdapter.Verify(x => x.SaveAsync(It.Is<HistoryUpdate>(h => h.Content.Contains(task.Id.ToString()))), Times.Once);
        }

        [Fact]
        public async Task SaveTaskAsync_ThrowsCustomException_WhenTaskLimitExceeded()
        {
            // Arrange
            var task = _fixture.Create<TaskProject>();
            var user = _fixture.Create<User>();

            _mockDbTaskReadAdapter.Setup(x => x.GetCountTaskByIdProjectAsync(task.ProjectId)).ReturnsAsync(21);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _taskService.SaveTaskAsync(task, user));
        }

        [Fact]
        public async Task UpdateTaskAsync_UpdatesTaskAndSavesLog()
        {
            // Arrange
            var task = _fixture.Create<TaskProject>();
            var user = _fixture.Create<User>();

            // Act
            await _taskService.UpdateTaskAsync(task, user);

            // Assert
            _mockDbTaskWriteAdapter.Verify(x => x.UpdateAsync(task), Times.Once);
            _mockLogUpdateAdapter.Verify(x => x.SaveAsync(It.Is<HistoryUpdate>(h => h.Content.Contains(task.Id.ToString()))), Times.Once);
        }

        [Fact]
        public async Task SaveCommentAsync_SavesCommentLog()
        {
            // Arrange
            var comment = _fixture.Create<Comments>();
            var user = _fixture.Create<User>();

            // Act
            await _taskService.SaveCommentAsync(comment, user);

            // Assert
            _mockLogUpdateAdapter.Verify(x => x.SaveAsync(It.Is<HistoryUpdate>(h => h.Content.Contains(comment.TaskProjectId.ToString()))), Times.Once);
        }

        [Fact]
        public async Task SaveTaskAsync_ThrowsCustomException_WhenTaskAlreadyExists()
        {
            // Arrange
            var task = _fixture.Create<TaskProject>();
            var user = _fixture.Create<User>();
            _mockDbTaskReadAdapter.Setup(x => x.GetCountTaskByIdProjectAsync(task.ProjectId)).ReturnsAsync(21);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _taskService.SaveTaskAsync(task, user));
        }

        [Fact]
        public async Task SaveCommentAsync_SavesCommentLogCorrectly()
        {
            // Arrange
            var comment = _fixture.Create<Comments>();
            var user = _fixture.Create<User>();

            // Act
            await _taskService.SaveCommentAsync(comment, user);

            // Assert
            _mockLogUpdateAdapter.Verify(x => x.SaveAsync(It.Is<HistoryUpdate>(h =>
                h.Content.Contains(comment.Comment) &&
                h.TaskProjectId == comment.TaskProjectId &&
                h.User == user.Name &&
                h.Action == CommonContants.COMMENT
            )), Times.Once);
        }

        [Fact]
        public async Task GetTaskAsync_ReturnsEmptyCollection_WhenNoTasksAvailable()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var emptyTasks = new List<TaskProject>();

            _mockDbTaskReadAdapter.Setup(x => x.GetAllTaskByProjectAsync(projectId)).ReturnsAsync(emptyTasks);

            // Act
            var result = await _taskService.GetTaskAsync(projectId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteTaskAsync_ThrowsException_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var user = _fixture.Create<User>();

            _mockDbTaskReadAdapter.Setup(x => x.GetByIdAsync(taskId)).ReturnsAsync((TaskProject)null);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _taskService.DeleteTaskAsync(taskId, user));
        }

        [Fact]
        public async Task SaveTaskAsync_HandlesMissingProjectIdCorrectly()
        {
            // Arrange
            var task = new TaskProject
            {
                Id = Guid.NewGuid(),
                Title = "Task with Missing Project",
                Description = "No Project ID",
                DueDate = DateTime.Now.AddDays(1),
                Status = StatusBase.Pending,
                Priority = Priority.Medium,
                ProjectId = Guid.Empty 
            };
            var user = _fixture.Create<User>();

            _mockDbTaskReadAdapter.Setup(x => x.GetCountTaskByIdProjectAsync(task.ProjectId)).ReturnsAsync(0);
            _mockDbTaskWriteAdapter.Setup(x => x.SaveAsync(task)).ReturnsAsync(task.Id);

            // Act
            await _taskService.SaveTaskAsync(task, user);

            // Assert
            _mockDbTaskWriteAdapter.Verify(x => x.SaveAsync(task), Times.Once);
        }

    }
}