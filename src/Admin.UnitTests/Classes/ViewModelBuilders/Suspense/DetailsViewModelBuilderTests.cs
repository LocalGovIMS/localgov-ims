using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Suspense;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.Suspense.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.Suspense.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.Suspense
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISuspenseService> _mockSuspenseService = new Mock<ISuspenseService>();

        private void SetupSuspenseService(Mock<ISuspenseService> service)
        {
            service.Setup(x => x.GetSuspense(It.IsAny<int>())).Returns(new SuspenseWrapper(
                new BusinessLogic.Entities.Suspense()
                {
                    Id = 1
                }
                ));
        }

        [TestMethod]
        public void OnBuildWithoutParamReturnsNull()
        {
            // Arrange
            var listViewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockSuspenseService.Object);

            // Act
            var result = listViewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }


        [TestMethod]
        public void OnBuildWithParamReturnsViewModel()
        {
            // Arrange
            SetupSuspenseService(_mockSuspenseService);

            var listViewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockSuspenseService.Object);

            var result = listViewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
