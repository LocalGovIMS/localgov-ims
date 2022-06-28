using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.TransactionImport.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImport.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImport
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportService> _mockTransactionImportService = new Mock<ITransactionImportService>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportService.Object,
                _mockTransactionImportTypeService.Object);
        }

        private void SetupServices()
        {
            _mockTransactionImportService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.TransactionImport.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.TransactionImport>()
                {
                    Items = new List<BusinessLogic.Entities.TransactionImport>()
                    {
                        new BusinessLogic.Entities.TransactionImport()
                        {
                            Id = 1
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
                });

            _mockTransactionImportTypeService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.TransactionImportType>()
                {
                    new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 1,
                        Name = "Transaction Import Type"
                    }
                });
        }

        [TestMethod]
        public void Build_without_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void Build_with_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(new Models.TransactionImport.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
