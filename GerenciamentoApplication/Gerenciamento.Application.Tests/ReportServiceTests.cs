using AutoFixture.AutoMoq;
using AutoFixture;
using Gerenciamento.Application.Constants;
using Gerenciamento.Domain.Adapters;
using Gerenciamento.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gerenciamento.Application.Tests
{
    public class ReportServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IReportAdapter> _reportAdapterMock;
        private readonly ReportService _reportService;

        public ReportServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _reportAdapterMock = _fixture.Freeze<Mock<IReportAdapter>>();
            _reportService = _fixture.Create<ReportService>();
        }

        [Fact]
        public async Task GetTaskByUserAsync_ValidManagerUser_ReturnsReportTasks()
        {
            // Arrange
            var user = _fixture.Build<User>()
                               .With(u => u.TypeUser, TypeUser.Manager)
                               .Create();
            var reportTasks = _fixture.CreateMany<ReportTask>();
            _reportAdapterMock.Setup(x => x.GetByIdAsync()).ReturnsAsync(reportTasks);

            // Act
            var result = await _reportService.GetTaskByUserAsync(user);

            // Assert
            Assert.Equal(reportTasks, result);
        }

        [Fact]
        public async Task GetTaskByUserAsync_NullUser_ThrowsCustomException()
        {
            // Arrange
            User user = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _reportService.GetTaskByUserAsync(user));
            Assert.Equal(ExceptionMessages.REGISTER_IS_EMPTY, exception.Message);
        }

        [Fact]
        public async Task GetTaskByUserAsync_NonManagerUser_ThrowsCustomException()
        {
            // Arrange
            var user = _fixture.Build<User>()
                               .With(u => u.TypeUser, TypeUser.Default)
                               .Create();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _reportService.GetTaskByUserAsync(user));
            Assert.Equal(ExceptionMessages.USER_NOT_AUTHORIZED, exception.Message);
        }
    }
}