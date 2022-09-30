using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.MetadataKey.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MetadataKey.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MetadataKey
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMetadataKeyService> _mockMetadataKeyService = new Mock<IMetadataKeyService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMetadataKeyService.Object);
        }

        private void SetupMetadataKeyService(Mock<IMetadataKeyService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.MetadataKey()
                {
                    Id = 1,
                    Name = "Name",
                    Description = "Description",
                    SystemType = false,
                    EntityType = (byte)MetadataKeyEntityType.Mop
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMetadataKeyService(_mockMetadataKeyService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
