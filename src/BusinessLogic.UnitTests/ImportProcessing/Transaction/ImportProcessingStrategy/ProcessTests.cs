﻿using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Services;
using FluentAssertions;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategy
{
    [TestClass]
    public class ProcessTests : TestBase
    {
        public ProcessTests()
        {
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(CompositeResolver.Create(new IFormatterResolver[]
            {
                // This can solve DateTime time zone problem
                NativeDateTimeResolver.Instance,
                ContractlessStandardResolver.Instance
            }));
        }

        [TestMethod]
        public void Process_CallsRuleEngineProcessOnce()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs(CreatableTransaction()));

            // Assert
            MockRuleEngine.Verify(x => x.Process(It.IsAny<ProcessedTransaction>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Process_WhenTheProcessedTransactionIsCreatable_CallsTransactionServiceCreateProcessedTransactionOnce()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs(CreatableTransaction()));

            // Assert
            MockTransactionService.Verify(x => x.CreateProcessedTransaction(It.IsAny<CreateProcessedTransactionArgs>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Another Test")]
        public void Process_WhenTheTransactionIsCreatableAndCreateProcessedTransactionReturnsAnUnsuccessfulResult_AnErrorIsRecordedAgainstTheImport(string errorMessage)
        {
            // Arrange
            SetupDependenciesForFailure(errorMessage);
            SetupStrategy();
            
            var args = GetArgs(CreatableTransaction());

            // Act
            Strategy.Process(args);

            // Assert
            args.Import.HasErrors().Should().BeTrue();
            args.Import.Errors().Count.Should().Be(1);
            args.Import.Errors()[0].Message.Should().Be(errorMessage);
        }

        [TestMethod]
        public void Process_WhenTheProcessedTransactionIsNotCreatable_CallsSuspenseServiceCreateOnce()
        {
            // Arrange
            SetupDependencies();
            SetupStrategy();

            // Act
            Strategy.Process(GetArgs(NonCreatableTransaction()));

            // Assert
            MockSuspenseService.Verify(x => x.Create(It.IsAny<CreateSuspenseArgs>()), Times.Once);
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("Another Test")]
        public void Process_WhenTheTransactionIsNotCreatableAndCreateSuspenseReturnsAnUnsuccessfulResult_AnErrorIsRecordedAgainstTheImport(string errorMessage)
        {
            // Arrange
            SetupDependenciesForFailure(errorMessage);
            SetupStrategy();

            var args = GetArgs(NonCreatableTransaction());
            
            // Act
            Strategy.Process(args);

            // Assert
            args.Import.HasErrors().Should().BeTrue();
            args.Import.Errors().Count.Should().Be(1);
            args.Import.Errors()[0].Message.Should().Be(errorMessage);
        }
    }
}
