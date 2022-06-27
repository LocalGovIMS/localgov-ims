using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeImportProcessingRuleService> _mockTransactionImportTypeImportProcessingRuleService = new Mock<ITransactionImportTypeImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockTransactionImportTypeImportProcessingRuleService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.TransactionImportTypeImportProcessingRule.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
                        {
                            Id = 1,
                            ImportProcessingRule = new BusinessLogic.Entities.ImportProcessingRule()
                            {
                                Name = "Import Processing Rule"
                            }
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
            var result = _viewModelBuilder.Build(new Models.TransactionImportTypeImportProcessingRule.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
