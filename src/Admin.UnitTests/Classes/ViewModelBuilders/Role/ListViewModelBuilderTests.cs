using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Role.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Role.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Role
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();

        private void SetupRoleService(Mock<IRoleService> service)
        {
            service.Setup(x => x.GetAllRoles()).Returns(new List<BusinessLogic.Entities.Role>()
            {
                {
                    new BusinessLogic.Entities.Role() {
                        DisplayName = "TestRole1",
                        Name = "Test Role 1",
                        RoleId = 0,
                        UserRoles = null
                    }
                },
                {
                    new BusinessLogic.Entities.Role() {
                        DisplayName = "TestRole2",
                        Name = "Test Role 2",
                        RoleId = 1,
                        UserRoles = null
                    }
                }
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupRoleService(_mockRoleService);

            var listViewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockRoleService.Object);

            // Act
            var result = listViewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(List<ViewModel>));
        }


        [TestMethod]
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange
            var listViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockRoleService.Object);

            var result = listViewModelBuilder.Build(1);

            // Assert
            result.Should().BeNull();
        }
    }
}
