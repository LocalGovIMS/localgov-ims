﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPaymentMetadata
{
    [TestClass]
    public class DetailsViewModelBuilderTests
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

        private void SetupService()
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
            SetupService();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
