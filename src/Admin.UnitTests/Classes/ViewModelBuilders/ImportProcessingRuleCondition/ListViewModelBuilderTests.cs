using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRuleCondition.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleCondition.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleCondition
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleConditionService> _mockImportProcessingRuleConditionService = new Mock<IImportProcessingRuleConditionService>();

        private void SetupService(Mock<IImportProcessingRuleConditionService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.ImportProcessingRuleCondition.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.ImportProcessingRuleCondition>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.ImportProcessingRuleCondition>()
                    {
                        new BusinessLogic.Entities.ImportProcessingRuleCondition()
                        {
                            Id = 1,
                            ImportProcessingRuleId = 1,
                            ImportProcessingRuleFieldId = 1,
                            Value = "Test value",
                            Field = new BusinessLogic.Entities.ImportProcessingRuleField()
                            {
                                DisplayName = "Test display name"
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
            SetupService(_mockImportProcessingRuleConditionService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }

        [TestMethod]
        public void Build_with_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupService(_mockImportProcessingRuleConditionService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleConditionService.Object);

            // Act
            var result = editViewModelBuilder.Build(new Models.ImportProcessingRuleCondition.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
