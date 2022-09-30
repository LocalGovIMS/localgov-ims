using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPaymentMetadata

{
    [TestClass]
    public class EditViewModelBuilderTests
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
            _mockMethodOfPaymentMetadataService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.MopMetadata()
                {
                    Id = 1,
                    MetadataKey = new BusinessLogic.Entities.MetadataKey()
                    {
                        Name = "Test key"
                    },
                    Value = "Test value",
                    MopCode = "M1"
                });

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
        public void Build_without_an_Id_returns_null()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object,
                _mockMethodOfPaymentMetadataKeyService.Object);

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_sets_the_Id_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_MopCode_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.MopCode.Should().Be("M1");
        }

        [TestMethod]
        public void Build_sets_the_Key_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.MetadataKeyName.Should().Be("Test key");
        }

        [TestMethod]
        public void Build_sets_the_Value_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Value.Should().Be("Test value");
        }
    }
}
