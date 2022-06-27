using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Moq;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.TransactionImportProcessor
{
    public class TestBase
    {
        protected readonly Mock<ILog> MockLog = new Mock<ILog>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<IRuleEngine> MockRuleEngine = new Mock<IRuleEngine>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<IValidator<TransactionImport>> MockTransactionImportValidator = new Mock<IValidator<TransactionImport>>();
    }
}
