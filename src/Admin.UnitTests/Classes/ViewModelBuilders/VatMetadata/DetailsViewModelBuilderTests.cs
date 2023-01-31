using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.VatMetadata.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.VatMetadata.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.VatMetadata
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatMetadataService> _mockVatMetadataService = new Mock<IVatMetadataService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatMetadataService.Object);
        }

        private void SetupService()
        {
            _mockVatMetadataService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.VatMetadata()
                {
                    Id = 1,
                    MetadataKey = new BusinessLogic.Entities.MetadataKey()
                    {
                        Name = "Test key"
                    },
                    Value = "Test value",
                    VatCode = "M1"
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
