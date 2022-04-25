using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.AccountReferenceValidator.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.AccountReferenceValidator.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.AccountReferenceValidator

{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();
        private readonly Mock<ICheckDigitConfigurationService> _mockCheckDigitConfigurationService = new Mock<ICheckDigitConfigurationService>();
        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
               _mockLogger.Object,
               _mockAccountReferenceValidatorService.Object,
               _mockCheckDigitConfigurationService.Object);
        }

        private void SetupServices()
        {
            _mockAccountReferenceValidatorService.Setup(x => x.Get(It.IsAny<int>())).Returns(
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

            _mockCheckDigitConfigurationService.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.CheckDigitConfiguration>()
                { 
                    new BusinessLogic.Entities.CheckDigitConfiguration()
                    {
                        Id = 1,
                        Name = "Test Configuration"
                    }
                });
        }

        [TestMethod]
        public void Build_without_an_Id_returns_empty_view_model()
        {
            // Arrange
            SetupServices();

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
        public void Build_sets_the_CharacterType_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.CharacterType.Should().Be(CharacterType.Alpha);
        }

        [TestMethod]
        public void Build_sets_the_CheckDigitConfigurationId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.CheckDigitConfigurationId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_InputMask_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.InputMask.Should().Be("######@");
        }

        [TestMethod]
        public void Build_sets_the_MaxLength_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.MaxLength.Should().Be(10);
        }

        [TestMethod]
        public void Build_sets_the_MinLength_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.MinLength.Should().Be(1);
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
        public void Build_sets_the_Regex_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Regex.Should().BeEmpty();
        }
    }
}

