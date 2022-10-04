using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.FundMessageMetadata.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMessageMetadata.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMessageMetadata
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMessageMetadataService> _mockFundMessageMetadataService = new Mock<IFundMessageMetadataService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundMessageMetadataService.Object);
        }

        private void SetupService()
        {
            _mockFundMessageMetadataService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.FundMessageMetadata.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.FundMessageMetadata>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.FundMessageMetadata>()
                    {
                        new BusinessLogic.Entities.FundMessageMetadata()
                        {
                            Id = 1,
                            MetadataKey = new BusinessLogic.Entities.MetadataKey()
                            {
                                Name = "Test key"
                            },
                            Value = "Test value",
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
            var result = _viewModelBuilder.Build(new Models.FundMessageMetadata.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
