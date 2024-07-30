using AutoFixture;
using AutoFixture.AutoMoq;
using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Moq;
using Xunit;

namespace Gerenciamento.Application.Tests
{
    public class ProjectServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IDbProjectReadAdapter> _dbProjectReadAdapterMock;
        private readonly Mock<IDbProjectWriteAdapter> _dbProjectWriteAdapterMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _dbProjectReadAdapterMock = _fixture.Freeze<Mock<IDbProjectReadAdapter>>();
            _dbProjectWriteAdapterMock = _fixture.Freeze<Mock<IDbProjectWriteAdapter>>();
            _projectService = new ProjectService(_dbProjectReadAdapterMock.Object, _dbProjectWriteAdapterMock.Object);
        }

        [Fact]
        public async Task DeleteProjectAsync_ProjectNotFound_ThrowsCustomException()
        {
            // Arrange
            var projectId = _fixture.Create<Guid>();
            _dbProjectReadAdapterMock.Setup(x => x.GetByIdAsync(projectId)).ReturnsAsync((Project)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _projectService.DeleteProjectAsync(projectId));
            Assert.Equal(ExceptionMessages.REGISTER_NOT_FOUND, exception.Message);
        }

        [Fact]
        public async Task DeleteProjectAsync_ProjectWithPendingTasks_ThrowsCustomException()
        {
            // Arrange
            var projectId = _fixture.Create<Guid>();
            var project = _fixture.Build<Project>()
                                  .With(p => p.Tasks, new List<TaskProject> { _fixture.Build<TaskProject>().With(t => t.Status, StatusBase.Pending).Create() })
                                  .Create();
            _dbProjectReadAdapterMock.Setup(x => x.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _projectService.DeleteProjectAsync(projectId));
            Assert.Equal(ExceptionMessages.PROJECT_WITH_PENDING_TASK, exception.Message);
        }

        [Fact]
        public async Task DeleteProjectAsync_SuccessfulDeletion()
        {
            // Arrange
            var projectId = _fixture.Create<Guid>();
            var project = _fixture.Build<Project>()
                                  .With(p => p.Tasks, new List<TaskProject>())
                                  .Create();
            _dbProjectReadAdapterMock.Setup(x => x.GetByIdAsync(projectId)).ReturnsAsync(project);

            // Act
            await _projectService.DeleteProjectAsync(projectId);

            // Assert
            _dbProjectWriteAdapterMock.Verify(x => x.DeleteAsync(project), Times.Once);
        }

        [Fact]
        public async Task GetProjectsAsync_ReturnsListOfProjects()
        {
            // Arrange
            var projects = _fixture.CreateMany<Project>().ToList();
            _dbProjectReadAdapterMock.Setup(x => x.GetAllAsync()).ReturnsAsync(projects);

            // Act
            var result = await _projectService.GetProjectsAsync();

            // Assert
            Assert.Equal(projects, result);
        }

        [Fact]
        public async Task SaveProjectAsync_ProjectIsNull_ThrowsCustomException()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _projectService.SaveProjectAsync(null));
            Assert.Equal(ExceptionMessages.REGISTER_IS_EMPTY, exception.Message);
        }

        [Fact]
        public async Task SaveProjectAsync_SuccessfulSave()
        {
            // Arrange
            var project = _fixture.Create<Project>();

            // Act
            await _projectService.SaveProjectAsync(project);

            // Assert
            _dbProjectWriteAdapterMock.Verify(x => x.SaveAsync(project), Times.Once);
        }
    }
}