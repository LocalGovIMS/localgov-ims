using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRuleTransactionImportType.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRuleTransactionImportType.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRuleTransactionImportType
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeImportProcessingRuleService> _mockTransactionImportTypeImportProcessingRuleService = new Mock<ITransactionImportTypeImportProcessingRuleService>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object,
                _mockTransactionImportTypeService.Object,
                _mockImportProcessingRuleService.Object);
        }

        private void SetupServices()
        {
            _mockTransactionImportTypeImportProcessingRuleService.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
                {
                    Id = 1,
                    TransactionImportType = new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 2,
                        Name = "Transaction Import Type Name"
                    },
                    ImportProcessingRule = new BusinessLogic.Entities.ImportProcessingRule()
                    {
                        Id = 3,
                        Name = "Import Processing Rule Name"
                    }
                });

            _mockTransactionImportTypeService.Setup(x => x.GetAll())
                .Returns(new List<BusinessLogic.Entities.TransactionImportType>()
                {
                    new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 1,
                        Name = "Transaction Import Type Name"
                    }
                });

            _mockImportProcessingRuleService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.ImportProcessingRule()
                {
                    Id = 1,
                    Name = "Import Processing Rule Name"
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
        public void Build_set_the_Id_property_correctly()
        {
            // Arrange
            SetupServices();
            
            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_TransactionImportTypeId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.TransactionImportTypeId.Should().Be(2);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleId_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleId.Should().Be(3);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleName_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleName.Should().Be("Import Processing Rule Name");
        }
    }
}
