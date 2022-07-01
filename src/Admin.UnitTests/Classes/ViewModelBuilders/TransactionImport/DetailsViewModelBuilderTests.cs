using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ViewModel = Admin.Models.TransactionImport.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImport.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImport
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportService> _mockTransactionImportService = new Mock<ITransactionImportService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportService.Object);
        }

        private void SetupService()
        {
            _mockTransactionImportService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.TransactionImport()
                {
                    Id = 1, 
                    TransactionImportType = new BusinessLogic.Entities.TransactionImportType()
                    {
                        Name = "Transaction Import Type Name"
                    },
                    CreatedByUser = new BusinessLogic.Entities.User()
                    {
                        UserName = "Test User"
                    },
                    CreatedDate = DateTime.Now,
                    EventLogs = new List<BusinessLogic.Entities.TransactionImportEventLog>(),
                    StatusHistories = new List<BusinessLogic.Entities.TransactionImportStatusHistory>() { 
                        new BusinessLogic.Entities.TransactionImportStatusHistory()
                        {
                            StatusId = (int)TransactionImportStatusEnum.Received,
                            CreatedDate = DateTime.Now,
                            CreatedByUser = new BusinessLogic.Entities.User()
                            {
                                UserName = "Test User"
                            }
                        }
                    }
                    ,
                    Description = "Description",
                    ExternalReference = "External Reference",
                    TotalAmount = 10,
                    TotalNumberOfTransactions = 2
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupService();

            // Act
             var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
