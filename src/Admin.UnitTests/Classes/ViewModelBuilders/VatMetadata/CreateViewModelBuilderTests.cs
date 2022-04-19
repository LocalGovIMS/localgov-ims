using Admin.Classes.ViewModelBuilders.VatMetadata;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.VatMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.VatMetadata.CreateViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.VatMetadata

{
    [TestClass]
    public class CreateViewModelBuilderTests
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

        private void SetupServices()
        {
            _mockVatMetadataService.Setup(x => x.GetMetadata())
                .Returns(new List<BusinessLogic.Models.Metadata>()
                {
                        new BusinessLogic.Models.Metadata()
                        {
                            Key = "A key",
                            Description = "A description"
                        }
                });
        }

        [TestMethod]
        public void Build_without_arguments_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(new CreateViewModelBuilderArgs() { VatCode = "M1" });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
