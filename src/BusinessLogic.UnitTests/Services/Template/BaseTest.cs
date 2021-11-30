﻿using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.TemplateService;

namespace BusinessLogic.UnitTests.Services.Template
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITemplateRowValidator> MockTemplateRowValidator = new Mock<ITemplateRowValidator>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockTemplateRowValidator.Object);

            return service;
        }
    }
}
