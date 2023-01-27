using BusinessLogic.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.EReturnDescription
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseEReturnDescriptionTest
    {
        protected readonly Mock<IEReturnTemplateRowService> MockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        public BusinessLogic.Validators.EReturnDescriptionValidator GetEReturnDescriptionValidator()
        {
            var validator = new BusinessLogic.Validators.EReturnDescriptionValidator(MockEReturnTemplateRowService.Object);

            return validator;
        }
    }
}
