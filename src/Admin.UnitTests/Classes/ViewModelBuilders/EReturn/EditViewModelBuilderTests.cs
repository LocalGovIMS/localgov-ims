using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.EReturn.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturn.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturn
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnService> _mockEReturnService = new Mock<IEReturnService>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnService.Object,
                _mockVatService.Object);
        }

        private void SetupEReturnService(Mock<IEReturnService> service, EReturnStatus status, EReturnType type)
        {
            service.Setup(x => x.GetEReturn(It.IsAny<int>())).Returns(new EReturnWrapper(
                new BusinessLogic.Entities.EReturn()
                {
                    Id = 1,
                    StatusId = (int)status,
                    TypeId = (int)type,
                    ProcessedTransactions = new List<BusinessLogic.Entities.ProcessedTransaction>()
                    {
                        {
                            new BusinessLogic.Entities.ProcessedTransaction()
                            {
                                Id = 1,
                                Amount = 100,
                                Narrative = "Processed",
                                TransactionReference = "T_Processed",
                                VatCode = "V1"
                            }
                        }
                    },
                    PendingTransactions = new List<BusinessLogic.Entities.PendingTransaction>()
                    {
                        {
                            new BusinessLogic.Entities.PendingTransaction()
                            {
                                Id = 2,
                                Amount = 200,
                                Narrative = "Pending",
                                TransactionReference = "T_Pending",
                                VatCode = "V2"
                            }
                        }
                    },
                    EReturnCashes = new List<BusinessLogic.Entities.EReturnCash>()
                    {
                        {
                            new BusinessLogic.Entities.EReturnCash()
                            {
                                Id = 1
                            }
                        }
                    },
                    EReturnCheques = new List<BusinessLogic.Entities.EReturnCheque>()
                    {
                        {
                            new BusinessLogic.Entities.EReturnCheque()
                            {
                                Id = 1
                            }
                        }
                    }
                }));
        }

        private void SetupVatService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(new List<BusinessLogic.Entities.Vat>()
            {
                new BusinessLogic.Entities.Vat()
                {
                    VatCode = "TEST",
                    Percentage= 20
                }
            });
        }

        [TestMethod]
        public void OnBuildReturnsNull()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.New, EReturnType.Cash);

            // Act
            var result = _viewModelBuilder.Build();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsModel()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.New, EReturnType.Cash);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
        }

        [TestMethod]
        public void OnBuildIssueReturnsEmptyModel()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
        }

        [TestMethod]
        public void OnBuildForSubmittedReturnsModel()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.Submitted, EReturnType.Cash);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
        }

        [TestMethod]
        public void OnBuildForSubmittedReturnsProcessedTransactions()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.Submitted, EReturnType.Cash);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
            result.Transactions[0].Description = "Processed";
        }

        [TestMethod]
        public void OnBuildForInProgressReturnsPendingTransactions()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.InProgress, EReturnType.Cash);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
            result.Transactions[0].Description = "Pending";
        }

        [TestMethod]
        public void OnBuildForInProgressChequeReturnsPendingTransactions()
        {
            // Arrange
            SetupEReturnService(_mockEReturnService, EReturnStatus.InProgress, EReturnType.Cheque);
            SetupVatService(_mockVatService);

            // Act
            var result = _viewModelBuilder.Build(1);

            //Assert
            result.Should().BeOfType<ViewModel>();
            result.Transactions[0].Description = "Pending";
        }
    }
}
