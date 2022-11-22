using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.EReturnTemplateRow.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.EReturnTemplateRow.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.EReturnTemplateRow
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateRowService> _mockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);
        }

        private void SetupService()
        {
            _mockEReturnTemplateRowService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.EReturnTemplateRow.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.TemplateRow>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.TemplateRow>()
                    {
                        new BusinessLogic.Entities.TemplateRow()
                        {
                            Id = 1,
                            TemplateId = 1,
                            Reference = "Reference",
                            ReferenceOverride = false,
                            Description = "Description",
                            DescriptionOverride = false,
                            VatCode = "V1",
                            VatOverride = false
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
            SetupService();

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
            SetupService();

            // Act
            var result = _viewModelBuilder.Build(new Models.EReturnTemplateRow.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
