using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.FundMessage.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.FundMessage.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.FundMessage
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundMessageService> _mockFundMessageService = new Mock<IFundMessageService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockFundMessageService.Object);
        }

        private void SetupFundMessageService(Mock<IFundMessageService> service)
        {
            service.Setup(x => x.GetById(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.FundMessage()
                {
                    Id = 1,
                    FundCode = "F1",
                    Fund = new BusinessLogic.Entities.Fund()
                    {
                        FundName = "Fund"
                    },
                    Message = "A message"
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
            SetupFundMessageService(_mockFundMessageService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
