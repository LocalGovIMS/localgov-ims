using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Transaction.RefundViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Transaction.RefundViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Transaction
{
    [TestClass]
    public class RefundViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionService> _mockTransactionService = new Mock<ITransactionService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionService.Object);
        }

        private void SetupTransactionService(Mock<ITransactionService> service)
        {
            var processedTransactions = new List<BusinessLogic.Entities.ProcessedTransaction>()
            {
                {
                    new BusinessLogic.Entities.ProcessedTransaction()
                    {
                        TransactionReference = "12345",
                        AccountReference = "ABCDE",
                        FundCode = "F1",
                        Amount = 100,
                        MopCode = "90",
                        Mop = new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "90",
                            Metadata = new List<BusinessLogic.Entities.MopMetadata>()
                            {
                                new BusinessLogic.Entities.MopMetadata()
                                {
                                    Value = "True",
                                    MetadataKey = new BusinessLogic.Entities.MetadataKey()
                                    {
                                        Name = MopMetadataKeys.IsARefundablePayment
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var pendingTransactions = new List<BusinessLogic.Entities.PendingTransaction>();
            var failedRefunds = new List<BusinessLogic.Entities.PendingTransaction>();

            var processedRefunds = new List<BusinessLogic.Entities.ProcessedTransaction>()
            {
                {
                    new BusinessLogic.Entities.ProcessedTransaction()
                    {
                        TransactionReference = "12345",
                        AccountReference = "ABCDE",
                        FundCode = "F1",
                        Amount = 1
                    }
                }
            };

            var transfers = new List<BusinessLogic.Entities.ProcessedTransaction>();

            service.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>())).Returns(
                new BusinessLogic.Models.Transaction(
                    processedTransactions,
                    pendingTransactions,
                    failedRefunds,
                    processedRefunds,
                    transfers,
                    string.Empty));
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
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
            SetupTransactionService(_mockTransactionService);

            var result = _viewModelBuilder.Build("12345");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
