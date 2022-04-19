using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRuleAction.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleActionService> _mockImportProcessingRuleActionService = new Mock<IImportProcessingRuleActionService>();
        private readonly Mock<IImportProcessingRuleFieldService> _mockImportProcessingRuleFieldService = new Mock<IImportProcessingRuleFieldService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object,
                _mockImportProcessingRuleFieldService.Object);
        }

        private void SetupServices()
        {
            _mockImportProcessingRuleActionService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRuleAction()
                {
                    Id = 1,
                    ImportProcessingRuleId = 1,
                    ImportProcessingRuleFieldId = 1,
                    Value = "Test value"
                });

            _mockImportProcessingRuleFieldService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.ImportProcessingRuleField>()
                {
                    new BusinessLogic.Entities.ImportProcessingRuleField()
                    {
                        Id = 1,
                        Name = "TestField",
                        DisplayName = "Test Field",
                        DisplayOrder = 1,
                        Type = "Text"
                    }
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
        public void Build_sets_the_ImportProcessingRuleId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleFieldId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleFieldId.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_Value_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Value.Should().Be("Test value");
        }
    }
}
