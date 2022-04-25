using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.AccountReferenceValidator.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountReferenceValidator.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountReferenceValidator
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountReferenceValidatorService.Object);
        }

        private void SetupService(Mock<IAccountReferenceValidatorService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.AccountReferenceValidator()
                {
                    Id = 1,
                    CharacterType = CharacterType.Alpha,
                    CheckDigitConfigurationId = 1,
                    InputMask = "######@",
                    MaxLength = 10,
                    MinLength = 1,
                    Name = "Test",
                    Regex = string.Empty
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
            SetupService(_mockAccountReferenceValidatorService);

            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
