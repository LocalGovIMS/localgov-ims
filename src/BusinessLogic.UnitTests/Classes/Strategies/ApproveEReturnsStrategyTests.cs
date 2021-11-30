using BusinessLogic.Classes.Strategies;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Strategies;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Classes.Strategies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ApproveEReturnsStrategyTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();
        private readonly Mock<ITransactionVatStrategy> _mockTransactionVatStrategy = new Mock<ITransactionVatStrategy>();

        public ApproveEReturnsStrategy ConstructorSetup()
        {
            return new ApproveEReturnsStrategy(
                _mockLogger.Object,
                _mockUnitOfWork.Object,
                _mockSecurityContext.Object,
                _mockTransactionVatStrategy.Object);
        }

        [TestMethod]
        public void ConstructorAcceptsCorrectArguments()
        {
            // Arrange
            try
            {
                // Act
                var strategy = new ApproveEReturnsStrategy(
                    _mockLogger.Object,
                    _mockUnitOfWork.Object,
                    _mockSecurityContext.Object,
                    _mockTransactionVatStrategy.Object);
            }
            catch (Exception)
            {
                // Assert;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExecuteGood()
        {
            _mockUnitOfWork.Setup(x => x.EReturns.GetEReturnsBeingProcessed(It.IsAny<Guid>()))
               .Returns(new List<EReturn>()
               {
                    new EReturn()
                    {
                        Id=3,
                        EReturnStatus = new EReturnStatus()
                        {Id =3 },
                        StatusId=3,
                        PendingTransactions = new List<PendingTransaction>()
                        {
                            new PendingTransaction()
                            {
                                Id=3,
                                StatusId=3,
                                Amount=1
                            }
                        }
                    }
               });

            _mockUnitOfWork.Setup(x => x.Transactions.AddRange(It.IsAny<List<ProcessedTransaction>>()));

            var controller = ConstructorSetup();

            var result = controller.Execute(new List<Tuple<int, string>>() { new Tuple<int, string>(3, "test") });

            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void ExecuteNoItemsError()
        {
            _mockUnitOfWork.Setup(x => x.EReturns.GetEReturnsBeingProcessed(It.IsAny<Guid>()))
                .Returns(new List<EReturn>());

            var controller = ConstructorSetup();

            var result = controller.Execute(new List<Tuple<int, string>>());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("You must choose some eReturns");
        }

        [TestMethod]
        public void ExecuteNotSubmittedEreturnError()
        {
            _mockUnitOfWork.Setup(x => x.EReturns.GetEReturnsBeingProcessed(It.IsAny<Guid>()))
                .Returns(new List<EReturn>()
                {
                    new EReturn()
                    {
                        EReturnStatus = new EReturnStatus()
                        {Id =1 }
                    }
                });

            var controller = ConstructorSetup();

            var result = controller.Execute(new List<Tuple<int, string>>());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Some of the eReturns selected are not in the correct state");
        }

        [TestMethod]
        public void ExecuteAlreadyProcessedEreturnError()
        {
            _mockUnitOfWork.Setup(x => x.EReturns.GetEReturnsBeingProcessed(It.IsAny<Guid>()))
                .Returns(new List<EReturn>()
                {
                    new EReturn()
                    {
                        EReturnStatus = new EReturnStatus()
                        {Id =3 },
                        ProcessedTransactions = new List<ProcessedTransaction>()
                        {
                            new ProcessedTransaction()
                            {
                                Id=1
                            }
                        }
                    }
                });

            var controller = ConstructorSetup();

            var result = controller.Execute(new List<Tuple<int, string>>());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to process all of the records requested.");
        }

        [TestMethod]
        public void ExecuteMissMatchedEreturnError()
        {
            _mockUnitOfWork.Setup(x => x.EReturns.GetEReturnsBeingProcessed(It.IsAny<Guid>()))
                .Returns(new List<EReturn>()
                {
                    new EReturn()
                    {
                        EReturnStatus = new EReturnStatus()
                        {Id =3 }
                    },
                    new EReturn()
                    {
                        EReturnStatus= new EReturnStatus()
                        {
                            Id=3
                        }
                    }
                });

            var controller = ConstructorSetup();

            var result = controller.Execute(new List<Tuple<int, string>>());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("Unable to process all of the records requested.");
        }
    }
}
