using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportProcessingRuleAction.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleActionService> _mockImportProcessingRuleActionService = new Mock<IImportProcessingRuleActionService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object);
        }

        private void SetupService(Mock<IImportProcessingRuleActionService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.ImportProcessingRuleAction.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.ImportProcessingRuleAction>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.ImportProcessingRuleAction>()
                    {
                        new BusinessLogic.Entities.ImportProcessingRuleAction()
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
            SetupService(_mockImportProcessingRuleActionService);

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
            SetupService(_mockImportProcessingRuleActionService);

            // Act
            var result = _viewModelBuilder.Build(new Models.ImportProcessingRuleAction.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
