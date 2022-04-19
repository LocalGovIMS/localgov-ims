using Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRuleAction.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleAction.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleAction
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleFieldService> _mockImportProcessingRuleFieldService = new Mock<IImportProcessingRuleFieldService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportProcessingRuleFieldService.Object);
        }

        private void SetupServices()
        {
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
        public void Build_without_arguments_returns_null()
        {
            // Arrange

            // Act
            var result = _viewModelBuilder.Build();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void Build_with_arguments_returns_a_view_model()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(new CreateViewModelBuilderArgs() { ImportProcessingRuleId = 1 });

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
