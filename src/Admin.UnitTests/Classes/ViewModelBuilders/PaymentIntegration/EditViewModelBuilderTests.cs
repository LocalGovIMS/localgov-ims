using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.PaymentIntegration.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.PaymentIntegration.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.PaymentIntegration

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IPaymentIntegrationService> _mockMopService = new Mock<IPaymentIntegrationService>();

        private void SetupMopService(Mock<IPaymentIntegrationService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.PaymentIntegration()
                {
                    Id = 1
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockMopService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = editViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
