using Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata.CreateViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPaymentMetadata

{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentMetadataService> _mockMethodOfPaymentMetadataService = new Mock<IMethodOfPaymentMetadataService>();

        private void SetupServices()
        {
            _mockMethodOfPaymentMetadataService.Setup(x => x.GetMetadata())
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
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object);

            // Act
            var result = editViewModelBuilder.Build(new CreateViewModelBuilderArgs() { MopCode = "M1" });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
