using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.Vat.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Vat.ListViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Vat
{
    [TestClass]
    public class ListViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatService> _mockVatService = new Mock<IVatService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockVatService.Object);
        }

        private void SetupMopService(Mock<IVatService> service)
        {
            service.Setup(x => x.GetAllCodes()).Returns(
                new List<BusinessLogic.Entities.Vat>()
                {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "V1",
                            Percentage = 0,
                            Disabled = false
                        }
                    }

                });

            service.Setup(x => x.GetAllCodes(It.IsAny<bool>())).Returns(
               new List<BusinessLogic.Entities.Vat>()
               {
                    {
                        new BusinessLogic.Entities.Vat()
                        {
                            VatCode = "V1",
                            Percentage = 0,
                            Disabled = false
                        }
                    }

               });
        }

        [TestMethod]
        public void OnBuildReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockVatService);

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
            var result = _viewModelBuilder.Build("V1");

            // Assert
            result.Should().BeNull();
        }
    }
}
