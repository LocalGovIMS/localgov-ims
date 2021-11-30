using BusinessLogic.Classes.Strategies;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Classes.Strategies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransactionVatStrategyTests
    {
        private readonly Mock<IFundRepository> _mockFundRepository = new Mock<IFundRepository>();
        private readonly Mock<IVatRepository> _mockVatRepository = new Mock<IVatRepository>();

        [TestMethod]
        public void TestVatCalculatedCorrectly()
        {
            //  Arrange
            _mockFundRepository.Setup(x => x.GetByFundCode("13")).Returns(new Fund()
            {
                FundCode = "13",
                VatOverride = false,
                Vat = new Vat()
                {
                    Percentage = (decimal)0.2,
                    VatCode = "W0"
                },
                VatCode = "W0"
            });
            _mockVatRepository.Setup(x => x.GetVatByVatCode("W0")).Returns(new Vat()
            {

                Percentage = (decimal)0.2,
                VatCode = "W0"

            });
            var strategy = new TransactionVatStrategy(_mockFundRepository.Object, _mockVatRepository.Object);
            var transaction = new ProcessedTransaction()
            {
                FundCode = "13",
                VatCode = "W0",
                Amount = (decimal)10.00
            };

            //  Act
            strategy.AddVatToTransaction(transaction);

            //Assert
            transaction.VatCode.Should().Be("W0");
            transaction.VatRate.Should().Be((float)0.2);
            transaction.VatAmount.Should().Be((decimal)1.67);
        }

        [TestMethod]
        public void VatCalculatedCorrectForNegative()
        {
            //  Arrange
            _mockFundRepository.Setup(x => x.GetByFundCode("13")).Returns(new Fund()
            {
                FundCode = "13",
                VatOverride = false,
                Vat = new Vat()
                {
                    Percentage = (decimal)0.2,
                    VatCode = "W0"
                },
                VatCode = "W0"
            });
            _mockVatRepository.Setup(x => x.GetVatByVatCode("W0")).Returns(new Vat()
            {

                Percentage = (decimal)0.2,
                VatCode = "W0"

            });
            var strategy = new TransactionVatStrategy(_mockFundRepository.Object, _mockVatRepository.Object);
            var transaction = new ProcessedTransaction()
            {
                FundCode = "13",
                VatCode = "W0",
                Amount = (decimal)-10.00
            };

            //  Act
            strategy.AddVatToTransaction(transaction);

            //Assert
            transaction.VatCode.Should().Be("W0");
            transaction.VatRate.Should().Be((float)0.2);
            transaction.VatAmount.Should().Be((decimal)-1.67);
        }

        [TestMethod]
        public void VatCalculatedCorrectlyForOverride()
        {
            //  Arrange
            _mockFundRepository.Setup(x => x.GetByFundCode("13")).Returns(new Fund()
            {
                FundCode = "13",
                VatOverride = true,
                Vat = new Vat()
                {
                    Percentage = (decimal)0.2,
                    VatCode = "W0"
                },
                VatCode = "W0"
            });
            _mockVatRepository.Setup(x => x.GetVatByVatCode("N0")).Returns(new Vat()
            {

                Percentage = (decimal)0.0,
                VatCode = "N0"
            });
            var strategy = new TransactionVatStrategy(_mockFundRepository.Object, _mockVatRepository.Object);
            var transaction = new ProcessedTransaction()
            {
                FundCode = "13",
                VatCode = "N0",
                Amount = (decimal)10.00
            };

            //  Act
            strategy.AddVatToTransaction(transaction);

            //Assert
            transaction.VatCode.Should().Be("N0");
            transaction.VatRate.Should().Be((float)0.0);
            transaction.VatAmount.Should().Be((decimal)0);
        }

        [TestMethod]
        public void VatCalculatedCorrectlyWhenCantBeOverridden()
        {
            //  Arrange
            _mockFundRepository.Setup(x => x.GetByFundCode("13")).Returns(new Fund()
            {
                FundCode = "13",
                VatOverride = false,
                Vat = new Vat()
                {
                    Percentage = (decimal)0.2,
                    VatCode = "W0"
                },
                VatCode = "W0"
            });
            _mockVatRepository.Setup(x => x.GetVatByVatCode("W0")).Returns(new Vat()
            {

                Percentage = (decimal)0.2,
                VatCode = "W0"
            });
            var strategy = new TransactionVatStrategy(_mockFundRepository.Object, _mockVatRepository.Object);
            var transaction = new ProcessedTransaction()
            {
                FundCode = "13",
                VatCode = "N0",
                Amount = (decimal)10.00
            };

            //  Act
            strategy.AddVatToTransaction(transaction);

            //Assert
            transaction.VatCode.Should().Be("W0");
            transaction.VatRate.Should().Be((float)0.2);
            transaction.VatAmount.Should().Be((decimal)1.67);
        }

    }
}
