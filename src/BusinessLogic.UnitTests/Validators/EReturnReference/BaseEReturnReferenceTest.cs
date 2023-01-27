using BusinessLogic.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.EReturnReference
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseEReturnReferenceTest
    {
        protected readonly Mock<IEReturnTemplateRowService> MockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        public BusinessLogic.Validators.EReturnReferenceValidator GetValidator()
        {
            var validator = new BusinessLogic.Validators.EReturnReferenceValidator(MockEReturnTemplateRowService.Object);

            return validator;
        }
    }
}
