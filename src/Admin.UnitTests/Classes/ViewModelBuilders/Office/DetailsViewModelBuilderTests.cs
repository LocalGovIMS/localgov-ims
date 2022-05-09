using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Office.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Office.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Office
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IOfficeService> _mockOfficeService = new Mock<IOfficeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockOfficeService.Object);
        }

        private void SetupMopService(Mock<IOfficeService> service)
        {
            service.Setup(x => x.Get(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.Office()
                {
                    OfficeCode = "V1"
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockOfficeService);

            // Act
            var result = _viewModelBuilder.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
