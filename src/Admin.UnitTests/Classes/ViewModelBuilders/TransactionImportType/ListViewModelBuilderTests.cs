using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.TransactionImportType.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImportType.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImportType
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportTypeService.Object);
        }

        private void SetupServices()
        {
            _mockTransactionImportTypeService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.TransactionImportType.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.TransactionImportType>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.TransactionImportType>()
                    {
                        new BusinessLogic.Entities.TransactionImportType()
                        {
                            Id = 1
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
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
            var result = _viewModelBuilder.Build(new Models.TransactionImportType.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
