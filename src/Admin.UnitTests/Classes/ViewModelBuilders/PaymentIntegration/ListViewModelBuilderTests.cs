using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.PaymentIntegration.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.PaymentIntegration.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.PaymentIntegration
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IPaymentIntegrationService> _mockMopService = new Mock<IPaymentIntegrationService>();

        private void SetupMopService(Mock<IPaymentIntegrationService> service)
        {
            service.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.PaymentIntegration>()
                {
                    {
                        new BusinessLogic.Entities.PaymentIntegration()
                        {
                            Id = 1
                        }
                    }

                });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockMopService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeOfType<List<ViewModel>>();
        }

        [TestMethod]
        public void OnBuildWithNoDataReturnsNull()
        {
            // Arrange          
            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeNull();
        }
    }
}
