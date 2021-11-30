using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Vat.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Vat.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Vat
{
    [TestClass]
    public class DetailsViewModelBuilderTests
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
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockVatService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockVatService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);

            // Act
            var result = viewModelBuidler.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
