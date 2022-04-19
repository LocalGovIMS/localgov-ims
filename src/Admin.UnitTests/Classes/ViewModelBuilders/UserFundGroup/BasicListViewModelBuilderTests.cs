using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Shared.BasicListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.UserFundGroup.BasicListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.UserFundGroup
{
    [TestClass]
    public class BasicListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserFundGroupService> _mockUserFundGroupService = new Mock<IUserFundGroupService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserFundGroupService.Object);
        }

        private void SetupUserFundGroupService(Mock<IUserFundGroupService> service)
        {
            service.Setup(x => x.GetUserFundGroups(It.IsAny<int>())).Returns(new List<BusinessLogic.Entities.UserFundGroup>()
            {
                {
                    new BusinessLogic.Entities.UserFundGroup() {
                        FundGroup = new BusinessLogic.Entities.FundGroup() { Name = "Test FundGroup 1" },
                        FundGroupId = 1,
                        User = null,
                        UserFundGroupId = 0,
                        UserId = 0
                    }
                },
                {
                    new BusinessLogic.Entities.UserFundGroup() {
                        FundGroup = new BusinessLogic.Entities.FundGroup() { Name = "Test FundGroup 2" },
                        FundGroupId = 2,
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

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupUserFundGroupService(_mockUserFundGroupService);

            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
