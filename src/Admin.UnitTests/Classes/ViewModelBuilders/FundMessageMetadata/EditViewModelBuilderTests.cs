﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.FundMessageMetadata.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMessageMetadata.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMessageMetadata
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMessageMetadataService> _mockFundMessageMetadataService = new Mock<IFundMessageMetadataService>();
        private readonly Mock<IMetadataKeyService> _mockMetadataKeyService = new Mock<IMetadataKeyService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundMessageMetadataService.Object,
                _mockMetadataKeyService.Object);
        }

        private void SetupServices()
        {
            _mockFundMessageMetadataService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.FundMessageMetadata()
                {
                    Id = 1,
                    MetadataKey = new BusinessLogic.Entities.MetadataKey()
                    {
                        Name = "Test key"
                    },
                    Value = "Test value",
                    FundMessageId = 1
                });

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

            _mockFundMessageMetadataService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.FundMessageMetadata.SearchCriteria>()))
               .Returns(new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.FundMessageMetadata>()
               {
                   Items = new List<BusinessLogic.Entities.FundMessageMetadata>()
                   {
                        new BusinessLogic.Entities.FundMessageMetadata()
                        {
                            Id = 1,
                            MetadataKey = new BusinessLogic.Entities.MetadataKey()
                            {
                                Id = 1,
                                Name = "Key1",
                                Description = "A description"
                            }
                        },
                        new BusinessLogic.Entities.FundMessageMetadata()
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
            result.FundMessageId.Should().Be(1);
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
