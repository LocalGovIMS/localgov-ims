using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.CheckDigitConfiguration.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.CheckDigitConfiguration.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.CheckDigitConfiguration

{
    [TestClass]
    public class EditViewModelBuilderTests
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

        private void SetupServices()
        {
            _mockCheckDigitConfigurationService.Setup(x => x.Get(It.IsAny<int>())).Returns(
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
            result.Should().BeAssignableTo<ViewModel>();
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_sets_the_Id_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ApplySubtraction_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ApplySubtraction.Should().BeFalse();
        }

        [TestMethod]
        public void Build_sets_the_Modulus_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Modulus.Should().Be(10);
        }

        [TestMethod]
        public void Build_sets_the_Name_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Name.Should().Be("Test");
        }

        [TestMethod]
        public void Build_sets_the_ResultSubstitutions_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ResultSubstitutions.Should().Be("10:A");
        }

        [TestMethod]
        public void Build_sets_the_SourceSubstitutions_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.SourceSubstitutions.Should().Be("A:1");
        }

        [TestMethod]
        public void Build_sets_the_Type_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Type.Should().Be(CheckDigitType.WeightedSum);
        }

        [TestMethod]
        public void Build_sets_the_Weightings_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Weightings.Should().Be("9,8,7,6,5,4,3,2,1,0,");
        }
    }
}

