﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.FundMetadata.ListViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMetadata.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMetadata
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMetadataService> _mockFundMetadataService = new Mock<IFundMetadataService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundMetadataService.Object);
        }

        private void SetupService()
        {
            _mockFundMetadataService.Setup(x => x.Search(It.IsAny<BusinessLogic.Models.FundMetadata.SearchCriteria>())).Returns(
                new BusinessLogic.Models.Shared.SearchResult<BusinessLogic.Entities.FundMetadata>()
                {
                    Items = new List<BusinessLogic.Entities.FundMetadata>()
                    {
                        new BusinessLogic.Entities.FundMetadata()
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
            var result = _viewModelBuilder.Build(new Models.FundMetadata.SearchCriteria());

            // Assert
            result.Should().BeOfType<ViewModel>();
            result.Count.Should().Be(1);
        }
    }
}
