using Admin.Classes.ViewModelBuilders.VatMetadata;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.VatMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.VatMetadata.CreateViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.VatMetadata

{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatMetadataService> _mockVatMetadataService = new Mock<IVatMetadataService>();
        private readonly Mock<IMetadataKeyService> _mockMetadataKeyService = new Mock<IMetadataKeyService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatMetadataService.Object,
                _mockMetadataKeyService.Object);
        }

        private void SetupServices()
        {
            _mockMetadataKeyService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MetadataKey.SearchCriteria>()))
                .Returns(new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.MetadataKey>()
                {
                    Items = new List<BusinessLogic.Entities.MetadataKey>()
                    {
                        new BusinessLogic.Entities.MetadataKey()
                        {
                            Id = 1,
                            Name = "Key1",
                            Description = "A description"
                        },
                        new BusinessLogic.Entities.MetadataKey()
                        {
                            Id = 4,
                            Name = "Key2",
                            Description = "A description"
                        },
                        new BusinessLogic.Entities.MetadataKey()
                        {
                            Id = 3,
                            Name = "Key3",
                            Description = "A description"
                        }
                    }
                });

            _mockVatMetadataService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.VatMetadata.SearchCriteria>()))
                .Returns(new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.VatMetadata>()
                {
                    Items = new List<BusinessLogic.Entities.VatMetadata>()
                    {
                        new BusinessLogic.Entities.VatMetadata()
                        {
                            Id = 1,
                            MetadataKey = new BusinessLogic.Entities.MetadataKey()
                            {
                                Id = 1,
                                Name = "Key1",
                                Description = "A description"
                            }
                        },
                        new BusinessLogic.Entities.VatMetadata()
                        {
                            Id = 2,
                            MetadataKey = new BusinessLogic.Entities.MetadataKey()
                            {
                                Id = 2,
                                Name = "Key2",
                                Description = "A description"
                            }
                        }
                    }
                });
        }

        [TestMethod]
        public void Build_without_arguments_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(new CreateViewModelBuilderArgs() { VatCode = "M1" });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
