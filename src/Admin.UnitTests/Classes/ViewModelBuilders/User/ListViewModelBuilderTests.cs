using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.User.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.User.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.User
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockUserService.Object);
        }

        private void SetupUserService(Mock<IUserService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.User.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.User>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.User>() {
                    {
                        new BusinessLogic.Entities.User()
                        {
                            UserId = 1
                        }
                    }
                },
                Page = 1,
                PageSize = 20
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService, 1);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupUserService(_mockUserService, 0);

            // Act
            var result = _viewModelBuilder.Build(new Models.User.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupUserService(_mockUserService, 1);

            // Act
            var result = _viewModelBuilder.Build(new Models.User.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
