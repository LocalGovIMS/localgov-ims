﻿using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.MethodOfPayment.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPayment.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPayment
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentService> _mockMopService = new Mock<IMethodOfPaymentService>();

        private void SetupMopService(Mock<IMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetMop(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.Mop()
                {
                    MopCode = "M1"
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockMopService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockMopService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            var result = viewModelBuidler.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
