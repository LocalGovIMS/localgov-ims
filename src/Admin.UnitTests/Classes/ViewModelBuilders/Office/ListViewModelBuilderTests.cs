using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Office.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Office.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Office
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IOfficeService> _mockOfficeService = new Mock<IOfficeService>();

        private void SetupMopService(Mock<IOfficeService> service)
        {
            service.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.Office>()
                {
                    {
                        new BusinessLogic.Entities.Office()
                        {
                            OfficeCode = "V1",
                            Name = "Test"
                        }
                    }

                });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockOfficeService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockOfficeService.Object);

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
                _mockOfficeService.Object);

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
                _mockOfficeService.Object);

            // Act
            var result = editViewModelBuilder.Build("V1");

            // Assert
            result.Should().BeNull();
        }
    }
}
