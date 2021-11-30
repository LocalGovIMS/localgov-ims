using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Fund.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Fund.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.Fund

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.Fund()
                {
                    FundCode = "F1"
                });
        }

        private void SetupVatService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(
                new List<BusinessLogic.Entities.Vat>()
                {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "v1"
                        }
                    }
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
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
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);

            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullFundReturnsViewModel()
        {
            // Arrange
            SetupVatService(_mockVatService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);

            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithVatCodes()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);

            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object);

            // Act
            var result = editViewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.VatCodes.Should().NotBeNull();
        }
    }
}
