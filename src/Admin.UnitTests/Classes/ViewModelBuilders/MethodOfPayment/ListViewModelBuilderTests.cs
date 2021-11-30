using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.MethodOfPayment.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPayment.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPayment
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentService> _mockMopService = new Mock<IMethodOfPaymentService>();

        private void SetupMopService(Mock<IMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetAllMops()).Returns(
                new List<BusinessLogic.Entities.Mop>()
                {
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M1",
                            Disabled = false
                        }
                    }

                });

            service.Setup(x => x.GetAllMops(It.IsAny<bool>())).Returns(
                new List<BusinessLogic.Entities.Mop>()
                {
                    {
                        new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "M1",
                            Disabled = false
                        }
                    }

                });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockMopService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeOfType<List<ViewModel>>();
        }

        [TestMethod]
        public void OnBuildWithNoDataReturnsNull()
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
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);

            // Act
            var result = editViewModelBuilder.Build("M1");

            // Assert
            result.Should().BeNull();
        }
    }
}
