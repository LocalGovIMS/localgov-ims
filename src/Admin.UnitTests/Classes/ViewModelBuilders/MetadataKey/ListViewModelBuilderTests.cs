using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MetadataKey.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MetadataKey.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MetadataKey
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMetadataKeyService> _mockMetadataKeyService = new Mock<IMetadataKeyService>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMetadataKeyService.Object);
        }

        private void SetupMetadataKeyService(Mock<IMetadataKeyService> service, int page)
        {
            service.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MetadataKey.SearchCriteria>())).Returns(new SearchResult<BusinessLogic.Entities.MetadataKey>()
            {
                Count = 1,
                Items = new List<BusinessLogic.Entities.MetadataKey>() {
                    {
                        new BusinessLogic.Entities.MetadataKey()
                        {
                            Id = 1,
                            Name = "Name",
                            Description = "Description",
                            SystemType = false,
                            EntityType = (byte)MetadataKeyEntityType.Mop
                        }
                    }
                },
                Page = 1,
                PageSize = 20
            });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsViewModel()
        {
            // Arrange
            SetupMetadataKeyService(_mockMetadataKeyService, 1);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMetadataKeyService(_mockMetadataKeyService, 0);

            // Act
            var result = _viewModelBuilder.Build(new Models.MetadataKey.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelForPage1()
        {
            // Arrange
            SetupMetadataKeyService(_mockMetadataKeyService, 1);

            // Act
            var result = _viewModelBuilder.Build(new Models.MetadataKey.SearchCriteria());

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
