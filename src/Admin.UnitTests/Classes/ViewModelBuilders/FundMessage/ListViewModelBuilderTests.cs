using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.FundMessage.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMessage.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMessage
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMessageService> _mockFundMessageService = new Mock<IFundMessageService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundMessageService.Object,
                _mockFundService.Object);
        }

        private void SetupFundMessageService(Mock<IFundMessageService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.FundMessage.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.FundMessage>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.FundMessage>() {
                    {
                        new BusinessLogic.Entities.FundMessage()
                        {
                            Id = 1,
                            FundCode = "F1",
                            Fund = new BusinessLogic.Entities.Fund()
                            {
                                FundName = "Fund"
                            },
                            Message = "A message"
                        }
                    }
                },
                Page = 1,
                PageSize = 20
            });
        }

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetAllFunds(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Fund>()
                {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1"
                        }
                    }
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupFundMessageService(_mockFundMessageService, 1);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupFundMessageService(_mockFundMessageService, 0);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build(new Models.FundMessage.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupFundMessageService(_mockFundMessageService, 1);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build(new Models.FundMessage.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
