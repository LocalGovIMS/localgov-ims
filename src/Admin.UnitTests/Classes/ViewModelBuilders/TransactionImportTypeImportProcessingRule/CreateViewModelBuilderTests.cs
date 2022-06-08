using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule.CreateViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImportTypeImportProcessingRule
{
    [TestClass]
    public class CreateViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportTypeService.Object,
                _mockImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockTransactionImportTypeService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.TransactionImportType()
                {
                    Id = 1,
                    ImportProcessingRules = new List<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
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
