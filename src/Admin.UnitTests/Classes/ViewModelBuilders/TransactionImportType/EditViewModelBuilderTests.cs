using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ViewModel = Admin.Models.TransactionImportType.EditViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.TransactionImportType.EditViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.TransactionImportType
{
    [TestClass]
    public class EditViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeService> _mockTransactionImportTypeService = new Mock<ITransactionImportTypeService>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

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
            _mockImportProcessingRuleService.Setup(x => x.GetAll(It.IsAny<bool>()))
                .Returns(new List<BusinessLogic.Entities.ImportProcessingRule>()
                {
                    new BusinessLogic.Entities.ImportProcessingRule()
                    {
                        Id = 1,
                        Name = "Import Processing Rule Name"
                    }
                });

            _mockTransactionImportTypeService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.TransactionImportType()
                {
                    Id = 1,
                    Name = "Transaction Import Type Name",
                    Description = "Description",
                    ExternalReference = "ER1"
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
        public void Build_sets_the_Name_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Name.Should().Be("Transaction Import Type Name");
        }

        [TestMethod]
        public void Build_sets_the_Description_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Description.Should().Be("Description");
        }

        [TestMethod]
        public void Build_sets_the_ExternalReference_property_correctly()
        {
            // Arrange
            SetupServices();

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ExternalReference.Should().Be("ER1");
        }
    }
}
