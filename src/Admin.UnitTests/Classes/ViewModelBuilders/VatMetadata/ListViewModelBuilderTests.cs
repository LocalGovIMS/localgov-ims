using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.VatMetadata.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.VatMetadata.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.VatMetadata
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatMetadataService> _mockVatMetadataService = new Mock<IVatMetadataService>();

        private void SetupService(Mock<IVatMetadataService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.VatMetadata.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.VatMetaData>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.VatMetaData>()
                    {
                        new BusinessLogic.Entities.VatMetaData()
                        {
                            Id = 1,
                            Key = "Test key",
                            Value = "Test value",
                        }
                    },
                    Count = 1,
                    Page = 1,
                    PageSize = 20
                });

            service.Setup(x => x.GetMetadata())
                .Returns(new List<BusinessLogic.Models.Metadata>()
                {
                    new BusinessLogic.Models.Metadata()
                    {
                        Key = "A key",
                        Description = "A description"
                    }
                });
        }

        [TestMethod]
        public void Build_without_search_criteria_returns_the_expected_result()
        {
            // Arrange
            SetupService(_mockVatMetadataService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatMetadataService.Object);

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
            SetupService(_mockVatMetadataService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatMetadataService.Object);

            // Act
            var result = editViewModelBuilder.Build(new Models.VatMetadata.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
