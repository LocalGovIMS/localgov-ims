using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Transfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionService> _mockTranscationService = new Mock<ITransactionService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
    }
}
