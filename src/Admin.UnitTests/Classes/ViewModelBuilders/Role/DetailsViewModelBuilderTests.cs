using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Role.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Role.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Role
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockRoleService.Object);
        }

        private void SetupRoleService(Mock<IRoleService> service)
        {
            service.Setup(x => x.GetRole(It.IsAny<int>())).Returns(new BusinessLogic.Entities.Role()
            {
                DisplayName = "TestRole1",
                Name = "Test Role 1",
                RoleId = 0,
                UserRoles = null
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupRoleService(_mockRoleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
