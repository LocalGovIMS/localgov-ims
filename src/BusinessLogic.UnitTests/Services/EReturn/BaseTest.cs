using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.EReturnService;

namespace BusinessLogic.UnitTests.Services.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();

        protected readonly Mock<IApproveEReturnsStrategy> MockApproveEReturnsStrategy = new Mock<IApproveEReturnsStrategy>();
        protected readonly Mock<IEReturnTemplateService> MockEReturnTemplateService = new Mock<IEReturnTemplateService>();
        protected readonly Mock<IEReturnReferenceValidator> MockEReturnReferenceValidator = new Mock<IEReturnReferenceValidator>();
        protected readonly Mock<IEReturnDescriptionValidator> MockEReturnDescriptionValidator = new Mock<IEReturnDescriptionValidator>();
        protected readonly Mock<IEmailService> MockEmailService = new Mock<IEmailService>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockApproveEReturnsStrategy.Object,
                MockEReturnTemplateService.Object,
                MockEReturnReferenceValidator.Object,
                MockEReturnDescriptionValidator.Object,
                MockEmailService.Object
                );

            return service;
        }
    }
}
