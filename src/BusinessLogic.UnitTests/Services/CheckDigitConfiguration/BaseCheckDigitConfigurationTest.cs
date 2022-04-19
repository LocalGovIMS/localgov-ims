﻿using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.CheckDigitConfigurationService;

namespace BusinessLogic.UnitTests.Services.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseCheckDigitConfigurationTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object);

            return service;
        }
    }
}
