using Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentMetadataService> _mockMethodOfPaymentMetadataService = new Mock<IMethodOfPaymentMetadataService>();
        private readonly Mock<IMetadataKeyService> _mockMethodOfPaymentMetadataKeyService = new Mock<IMetadataKeyService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object,
                _mockMethodOfPaymentMetadataKeyService.Object);
        }

        private void SetupServices()
        {
            _mockMethodOfPaymentMetadataKeyService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MetadataKey.SearchCriteria>()))
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

            _mockMethodOfPaymentMetadataService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.MethodOfPaymentMetadata.SearchCriteria>()))
                .Returns(new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.MopMetadata>()
                {
                    Items = new List<BusinessLogic.Entities.MopMetadata>()
                    {
                        new BusinessLogic.Entities.MopMetadata()
                        {
                            Id = 1,
                            MetadataKey = new BusinessLogic.Entities.MetadataKey()
                            {
                                Id = 1,
                                Name = "Key1",
                                Description = "A description"
                            }
                        },
                        new BusinessLogic.Entities.MopMetadata()
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
            var result = _viewModelBuilder.Build(new CreateViewModelBuilderArgs() { MopCode = "M1" });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
