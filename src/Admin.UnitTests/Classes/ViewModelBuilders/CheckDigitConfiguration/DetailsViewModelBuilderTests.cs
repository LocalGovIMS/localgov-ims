using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.CheckDigitConfiguration.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.CheckDigitConfiguration.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.CheckDigitConfiguration
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ICheckDigitConfigurationService> _mockCheckDigitConfigurationService = new Mock<ICheckDigitConfigurationService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockCheckDigitConfigurationService.Object);
        }

        private void SetupService(Mock<ICheckDigitConfigurationService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.CheckDigitConfiguration()
                {
                    Id = 1,
                    ApplySubtraction = false,
                    Modulus = 10,
                    Name = "Test",
                    ResultSubstitutions = "10:A",
                    SourceSubstitutions = "A:1",
                    Type = (int)CheckDigitType.WeightedSum,
                    Weightings = "9,8,7,6,5,4,3,2,1,0,"
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupService(_mockCheckDigitConfigurationService);

            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
