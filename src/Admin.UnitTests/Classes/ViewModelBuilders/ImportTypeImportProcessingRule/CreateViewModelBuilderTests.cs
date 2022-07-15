using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportTypeImportProcessingRule.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();
        private readonly Mock<IImportTypeService> _mockImportTypeService = new Mock<IImportTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportTypeService.Object,
                _mockImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockImportTypeService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.ImportType()
                {
                    Id = 1,
                    ImportProcessingRules = new List<BusinessLogic.Entities.ImportTypeImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.ImportTypeImportProcessingRule()
                        {
                            Id = 1,
                            ImportProcessingRule = new BusinessLogic.Entities.ImportProcessingRule()
                        }
                    }
                });

            _mockImportProcessingRuleService.Setup(x => x.GetAll(It.IsAny<bool>()))
                .Returns(new List<BusinessLogic.Entities.ImportProcessingRule>()
                {
                    new BusinessLogic.Entities.ImportProcessingRule()
                    {
                        Id = 1
                    }
                });
        }

        [TestMethod]
        public void Build_without_arguments_throws_NotImplementedException()
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
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }
    }
}
