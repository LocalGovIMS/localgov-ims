using BusinessLogic.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.TransactionFee
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTransactionFeeTest
    {
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();

        public BusinessLogic.Validators.TransactionFeeValidator GetValidator()
        {
            var validator = new BusinessLogic.Validators.TransactionFeeValidator(MockTransactionService.Object);

            return validator;
        }
    }
}
