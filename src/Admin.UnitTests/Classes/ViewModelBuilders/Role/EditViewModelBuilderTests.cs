using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Role.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Role.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Role

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();

        private void SetupRoleService(Mock<IRoleService> service)
        {
            service.Setup(x => x.GetRole(It.IsAny<int>())).Returns(new BusinessLogic.Entities.Role()
            {
                DisplayName = "TestRole",
                Name = "Test Role",
                RoleId = 0,
                UserRoles = null
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockRoleService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupRoleService(_mockRoleService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockRoleService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullRoleReturnsViewModel()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockRoleService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
