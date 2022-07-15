using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRuleImportType.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleImportType.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleImportType
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportTypeImportProcessingRuleService> _mockImportTypeImportProcessingRuleService = new Mock<IImportTypeImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportTypeImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockImportTypeImportProcessingRuleService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.ImportTypeImportProcessingRule.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.ImportTypeImportProcessingRule>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.ImportTypeImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.ImportTypeImportProcessingRule()
                        {
                            Id = 1,
                            ImportType = new BusinessLogic.Entities.ImportType()
                            {
                                Name = "Transaction Import Type"
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
            var result = _viewModelBuilder.Build(new Models.ImportProcessingRuleImportType.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
