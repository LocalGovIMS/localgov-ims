using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRule.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRule.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRule
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);
        }

        private void SetupService(Mock<IImportProcessingRuleService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.ImportProcessingRule.SearchCriteria>())).Returns(
                new SearchResult<BusinessLogic.Entities.ImportProcessingRule>()
                {
                    Items = new List<BusinessLogic.Entities.ImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.ImportProcessingRule()
                        {
                            Id = 1
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 10
                });
        }

        [TestMethod]
        public void Build_without_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleService);

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
            SetupService(_mockImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(new Models.ImportProcessingRule.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
