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

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockMopService.Object);
        }

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

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeOfType<List<ViewModel>>();
        }

        [TestMethod]
        public void OnBuildWithNoDataReturnsNull()
        {
            // Arrange          

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build("M1");

            // Assert
            result.Should().BeNull();
        }
    }
}
