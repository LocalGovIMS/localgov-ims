using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Transaction.TransferViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Transaction.TransferViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Transaction
{
    [TestClass]
    public class TransferViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockVatService.Object);
        }

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetAllFunds()).Returns(
                new List<BusinessLogic.Entities.Fund>()
                {
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F1",
                            FundName = "Fund1",
                            VatCode = "W0"
                        }
                    },
                    {
                        new BusinessLogic.Entities.Fund()
                        {
                            FundCode = "F2",
                            FundName = "Fund2",
                            VatCode = "N0"
                        }
                    }
                }

            );
        }

        private void SetupVatService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(
                new List<BusinessLogic.Entities.Vat>()
                {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "W0",
                            Percentage = (decimal) 0.0200
                        }
                    },
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "N0",
                            Percentage = (decimal) 0.0000
                        }
                    }
                }

            );
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);
            SetupVatService(_mockVatService);

            var result = _viewModelBuilder.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
