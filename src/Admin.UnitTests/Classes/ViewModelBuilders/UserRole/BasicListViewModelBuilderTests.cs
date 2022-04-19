using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Shared.BasicListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserRole.BasicListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.UserRole
{
    [TestClass]
    public class BasicListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserRoleService> _mockUserRoleService = new Mock<IUserRoleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserRoleService.Object);
        }

        private void SetupUserRoleService(Mock<IUserRoleService> service)
        {
            service.Setup(x => x.GetUserRoles(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserRole>()
            {
                {
                    new BusinessLogic.Entities.UserRole() {
                        Role = new BusinessLogic.Entities.Role() { Name = "Test Role 1" },
                        RoleId = 1,
                        User = null,
                        UserId = 0,
                        UserRoleId = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserRole() {
                        Role = new BusinessLogic.Entities.Role() { Name = "Test Role 2" },
                        RoleId = 2,
                        User = null,
                        UserId = 0,
                        UserRoleId = 0
                    }
                }
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
            SetupUserRoleService(_mockUserRoleService);

            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
