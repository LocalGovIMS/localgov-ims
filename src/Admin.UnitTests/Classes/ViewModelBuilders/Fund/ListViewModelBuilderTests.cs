using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Fund.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Fund.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Fund
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private void SetupFundService(Mock<IFundService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.Fund.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.Fund>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.Fund>() {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1"
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
            SetupFundService(_mockFundService, 1);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService, 0);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);

            // Act
            var result = viewModelBuidler.Build(new Admin.Models.Fund.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupFundService(_mockFundService, 1);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);

            // Act
            var result = viewModelBuidler.Build(new Admin.Models.Fund.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
