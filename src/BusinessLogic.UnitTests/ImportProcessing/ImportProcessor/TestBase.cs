﻿using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Moq;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.ImportProcessor
{
    public class TestBase
    {
        protected readonly Mock<ILog> MockLog = new Mock<ILog>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<Func<string, IImportInitialisationStrategy>> MockImportInitialisationStrategyFactory = new Mock<Func<string, IImportInitialisationStrategy>>();
        protected readonly Mock<Func<string, IImportProcessingStrategy>> MockImportProcessingStrategyFactory = new Mock<Func<string, IImportProcessingStrategy>>();
        protected readonly Mock<Func<string, IValidator<ImportProcessingStrategyValidatorArgs>>> MockImportProcessingValidatorFactory = new Mock<Func<string, IValidator<ImportProcessingStrategyValidatorArgs>>>();

        protected readonly Mock<IImportProcessingStrategy> MockImportProcessingStrategy = new Mock<IImportProcessingStrategy>();
        protected readonly Mock<IValidator<ImportProcessingStrategyValidatorArgs>> MockImportProcessingValidator = new Mock<IValidator<ImportProcessingStrategyValidatorArgs>>();
    }
}
