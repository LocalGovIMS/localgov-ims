using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Office.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Office.EditViewModelBuilder;


namespace Admin.UnitTests.Classes.ViewModelBuilders.Office

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IOfficeService> _mockOfficeService = new Mock<IOfficeService>();

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
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockOfficeService.Object);

            // Act
            var result = editViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockOfficeService);

            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockOfficeService.Object);

            // Act
            var result = editViewModelBuilder.Build("V1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange
            var editViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockOfficeService.Object);

            // Act
            var result = editViewModelBuilder.Build("V1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
