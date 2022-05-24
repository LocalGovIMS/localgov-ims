using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.FundMessage.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMessage.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMessage
{
    [TestClass]
    public class EditViewModelBuilderTests
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

        private void SetupFundMessageService(Mock<IFundMessageService> service)
        {
            service.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.FundMessage()
                {
                    Id = 1,
                    FundCode = "F1",
                    Fund = new BusinessLogic.Entities.Fund()
                    {
                        FundName = "Fund"
                    },
                    Message = "A message"
                });
        }

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetAllFunds()).Returns(
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
            SetupFundMessageService(_mockFundMessageService);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullFundMessageReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModel()
        {
            // Arrange
            SetupFundMessageService(_mockFundMessageService);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithFundCodes()
        {
            // Arrange
            SetupFundMessageService(_mockFundMessageService);
            SetupFundService(_mockFundService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.Funds.Should().NotBeNull();
        }
    }
}
