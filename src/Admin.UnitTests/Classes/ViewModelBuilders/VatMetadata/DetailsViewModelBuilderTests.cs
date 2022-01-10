﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.VatMetadata.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.VatMetadata.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.VatMetadata
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatMetadataService> _mockVatMetadataService = new Mock<IVatMetadataService>();

        private void SetupService(Mock<IVatMetadataService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.VatMetaData()
                {
                    Id = 1,
                    Key = "Test key",
                    Value = "Test value",
                    VatCode = "M1"
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
        public void Build_without_an_Id_returns_null()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockVatMetadataService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockVatMetadataService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatMetadataService.Object);

            var result = viewModelBuidler.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}