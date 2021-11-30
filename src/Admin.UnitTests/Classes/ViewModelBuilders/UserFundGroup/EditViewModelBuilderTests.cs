using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.UserFundGroup.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserFundGroup.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.UserFundGroup

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();
        private readonly Mock<IUserFundGroupService> _mockUserFundGroupService = new Mock<IUserFundGroupService>();

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

        private void SetupFundGroupService(Mock<IFundGroupService> service)
        {
            service.Setup(x => x.GetAllFundGroups()).Returns(new List<BusinessLogic.Entities.FundGroup>()
            {
                {
                    new BusinessLogic.Entities.FundGroup() {
                        FundGroupFunds = null,
                        FundGroupId = 0,
                        Name = "Fund Group 1",
                        UserFundGroups = null
                    }
                },
                {
                    new BusinessLogic.Entities.FundGroup() {
                        FundGroupFunds = null,
                        FundGroupId = 1,
                        Name = "Fund Group 2",
                        UserFundGroups = null
                    }
                }
            });
        }

        private void SetupUserFundGroupService(Mock<IUserFundGroupService> service)
        {
            service.Setup(x => x.GetUserFundGroups(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserFundGroup>()
            {
                {
                    new BusinessLogic.Entities.UserFundGroup() {
                        FundGroup = null,
                        FundGroupId = 0,
                        User = null,
                        UserFundGroupId = 0,
                        UserId = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserFundGroup() {
                      FundGroup = null,
                        FundGroupId = 1,
                        User = null,
                        UserFundGroupId = 1,
                        UserId = 0
                    }
                }
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockFundGroupService.Object,
                _mockUserFundGroupService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupFundGroupService(_mockFundGroupService);
            SetupUserFundGroupService(_mockUserFundGroupService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockFundGroupService.Object,
                _mockUserFundGroupService.Object);

            // Act
            var result = editViewModelBuilder.Build(2);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserReturnsViewModel()
        {
            // Arrange
            SetupFundGroupService(_mockFundGroupService);
            SetupUserFundGroupService(_mockUserFundGroupService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockFundGroupService.Object,
                _mockUserFundGroupService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullUserFundGroupsReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService);
            SetupFundGroupService(_mockFundGroupService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object,
                _mockFundGroupService.Object,
                _mockUserFundGroupService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
