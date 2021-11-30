using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Vat.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Vat.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.Vat

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private void SetupMopService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetByVatCode(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.Vat()
                {
                    VatCode = "V1"
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockVatService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Build("V1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Build("V1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
