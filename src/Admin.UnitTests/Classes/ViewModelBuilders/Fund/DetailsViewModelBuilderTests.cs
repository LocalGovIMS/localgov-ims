using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Fund.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Fund.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Fund
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private void SetupFundService(Mock<IFundService> service)
        {
            service.Setup(x => x.GetByFundCode(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.Fund()
                {
                    FundCode = "F1"
                });
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var viewModelBuidler = new ViewModelBuilder(
               _mockLogger.Object,
               _mockFundService.Object);

            // Act
            var result = viewModelBuidler.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupFundService(_mockFundService);

            var viewModelBuidler = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundService.Object);

            var result = viewModelBuidler.Build("Ref");

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
