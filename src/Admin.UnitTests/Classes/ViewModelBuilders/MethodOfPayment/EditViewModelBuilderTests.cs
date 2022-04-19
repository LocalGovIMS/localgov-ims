using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.MethodOfPayment.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.MethodOfPayment.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.MethodOfPayment
{
    [TestClass]
    public class EditViewModelBuilderTests
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

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupMopService(_mockMopService);

            // Act
            var result = _viewModelBuilder.Build("M1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void OnBuildWithParamReturnsViewModelIfDataIsNull()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build("M1");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
