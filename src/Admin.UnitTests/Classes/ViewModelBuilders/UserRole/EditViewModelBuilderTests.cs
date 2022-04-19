using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.UserRole.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserRole.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.UserRole

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();
        private readonly Mock<IUserRoleService> _mockUserRoleService = new Mock<IUserRoleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockRoleService.Object,
                _mockUserRoleService.Object);
        }

        private void SetupUserService(Mock<IUserService> service)
        {
            service.Setup(x => x.GetUser(It.IsAny<int>())).Returns(new BusinessLogic.Entities.User()
            {
                Disabled = false,
                DisplayName = string.Empty,
                ExpiryDays = 0,
                LastLogin = null,
                UserFundGroups = null,
                UserId = 0,
                UserName = string.Empty,
                UserRoles = null
            });
        }

        private void SetupRoleService(Mock<IRoleService> service)
        {
            service.Setup(x => x.GetAllRoles()).Returns(new List<BusinessLogic.Entities.Role>()
            {
                {
                    new BusinessLogic.Entities.Role() {
                        DisplayName = "Role 1",
                        Name = "Role 1",
                        RoleId = 0,
                        UserRoles = null
                    }
                },
                {
                    new BusinessLogic.Entities.Role() {
                        DisplayName = "Role 2",
                        Name = "Role 2",
                        RoleId = 0,
                        UserRoles = null
                    }
                }
            });
        }

        private void SetupUserRoleService(Mock<IUserRoleService> service)
        {
            service.Setup(x => x.GetUserRoles(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserRole>()
            {
                {
                    new BusinessLogic.Entities.UserRole() {
                        Role = null,
                        RoleId = 0,
                        User = null,
                        UserId = 0,
                        UserRoleId = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserRole() {
                        Role = null,
                        RoleId = 1,
                        User = null,
                        UserId = 0,
                        UserRoleId = 1
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
            SetupUserService(_mockUserService);
            SetupRoleService(_mockRoleService);
            SetupUserRoleService(_mockUserRoleService);

            // Act
            var result = _viewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserReturnsViewModel()
        {
            // Arrange
            SetupRoleService(_mockRoleService);
            SetupUserRoleService(_mockUserRoleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserRolesReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupRoleService(_mockRoleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
