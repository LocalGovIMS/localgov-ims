using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.ImportProcessingRule.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportProcessingRule.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportProcessingRule
{
    [TestClass]
    public class EditViewModelBuilderTests
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
                _mockImportProcessingRuleService.Object,
                _mockTransactionImportTypeService.Object);
        }

        private void SetupImportProcessingRuleService(Mock<IImportProcessingRuleService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportProcessingRule()
                {
                    Id = 1,
                    TransactionImportTypes = new List<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()
                    {
                        new BusinessLogic.Entities.TransactionImportTypeImportProcessingRule()
                        {
                            Id = 1
                        }
                    }
                });
        }

        private void SetupTransactionImportTypeService(Mock<ITransactionImportTypeService> service)
        {
            service.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.TransactionImportType>()
                {
                    new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 1,
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
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_with_an_Id_returns_a_view_model()
        {
            // Arrange
            SetupImportProcessingRuleService(_mockImportProcessingRuleService);
            SetupTransactionImportTypeService(_mockTransactionImportTypeService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_sets_the_TransactionImportTypesAreAvailableToAdd_to_false_when_there_are_no_TransactionImportTypes_available_to_add()
        {
            // Arrange
            SetupImportProcessingRuleService(_mockImportProcessingRuleService);
            SetupTransactionImportTypeService(_mockTransactionImportTypeService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.TransactionImportTypesAreAvailableToAdd.Should().BeFalse();
        }

        [TestMethod]
        public void Build_sets_the_TransactionImportTypesAreAvailableToAdd_to_true_when_there_are_TransactionImportTypes_available_to_add()
        {
            // Arrange
            SetupImportProcessingRuleService(_mockImportProcessingRuleService);
            _mockTransactionImportTypeService.Setup(x => x.GetAll()).Returns(
                new List<BusinessLogic.Entities.TransactionImportType>()
                {
                    new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 1,
                    },
                    new BusinessLogic.Entities.TransactionImportType()
                    {
                        Id = 2,
                    }
                });

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.TransactionImportTypesAreAvailableToAdd.Should().BeTrue();
        }
    }
}
