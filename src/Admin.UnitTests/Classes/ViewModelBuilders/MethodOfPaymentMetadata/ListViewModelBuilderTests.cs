using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentMetadataService> _mockMethodOfPaymentMetadataService = new Mock<IMethodOfPaymentMetadataService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object);
        }

        private void SetupService(Mock<IMethodOfPaymentMetadataService> service)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.MopMetadata>()
                {
                    Items = new System.Collections.Generic.List<BusinessLogic.Entities.MopMetadata>()
                    {
                        new BusinessLogic.Entities.MopMetadata()
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
            SetupService(_mockMethodOfPaymentMetadataService);

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
            SetupService(_mockMethodOfPaymentMetadataService);

            // Act
            var result = _viewModelBuilder.Build(new Models.MethodOfPaymentMetadata.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
