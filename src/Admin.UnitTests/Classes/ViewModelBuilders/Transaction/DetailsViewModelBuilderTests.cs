using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Transaction.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Transaction.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Transaction
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionService> _mockTransactionService = new Mock<ITransactionService>();

        private void SetupTransactionService(Mock<ITransactionService> service)
        {
            var processedTransactions = new List<BusinessLogic.Entities.ProcessedTransaction>();
            var pendingTransactions = new List<BusinessLogic.Entities.PendingTransaction>();
            var processedRefunds = new List<BusinessLogic.Entities.ProcessedTransaction>();
            var failedRefunds = new List<BusinessLogic.Entities.PendingTransaction>();
            var transfers = new List<BusinessLogic.Entities.ProcessedTransaction>();

            service.Setup(x => x.GetTransactionByPspReference(It.IsAny<string>())).Returns(
                new BusinessLogic.Models.Transaction(
                    processedTransactions,
                    failedRefunds,
                    pendingTransactions,
                    processedRefunds,
                    transfers,
                    string.Empty));
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockTransactionService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupTransactionService(_mockTransactionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionService.Object);

            var result = viewModelBuidler.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
