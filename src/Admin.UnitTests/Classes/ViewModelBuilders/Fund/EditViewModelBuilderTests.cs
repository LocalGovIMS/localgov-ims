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
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object,
                _mockAccountReferenceValidatorService.Object);
        }

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

        private void SetupAccountReferenceValidatorService(Mock<IAccountReferenceValidatorService> service)
        {
            service.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.AccountReferenceValidator>()
                {
                    {
                        new BusinessLogic.Entities.AccountReferenceValidator()
                        {
                            Id = 1,
                            Name = "Validator"
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
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupAccountReferenceValidatorService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamNullFundReturnsViewModel()
        {
            // Arrange
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build("F1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupAccountReferenceValidatorService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithVatCodes()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupAccountReferenceValidatorService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.VatCodes.Should().NotBeNull();
        }

        [TestMethod]
        public void OnRebuildReturnsViewModelWithAccountReferenceValidators()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);
            SetupAccountReferenceValidatorService(_mockAccountReferenceValidatorService);

            // Act
            var result = _viewModelBuilder.Rebuild(new ViewModel());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
            result.VatCodes.Should().NotBeNull();
        }
    }
}
