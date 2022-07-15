using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModel = Admin.Models.ImportTypeImportProcessingRule.DetailsViewModel;
using ViewModelBuilder = Admin.Classes.ViewModelBuilders.ImportTypeImportProcessingRule.DetailsViewModelBuilder;

namespace Admin.UnitTests.Classes.ViewModelBuilders.ImportTypeImportProcessingRule
{
    [TestClass]
    public class DetailsViewModelBuilderTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportTypeImportProcessingRuleService> _mockImportTypeImportProcessingRuleService = new Mock<IImportTypeImportProcessingRuleService>();

        private ViewModelBuilder _viewModelBuilder;

        [TestInitialize]
        public void TestInitialise()
        {
            _viewModelBuilder = new ViewModelBuilder(
                _mockLogger.Object,
                _mockImportTypeImportProcessingRuleService.Object);
        }

        private void SetupService(Mock<IImportTypeImportProcessingRuleService> service)
        {
            service.Setup(x => x.Get(It.IsAny<int>())).Returns(
                new BusinessLogic.Entities.ImportTypeImportProcessingRule()
                {
                    Id = 1,
                    ImportType = new BusinessLogic.Entities.ImportType()
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
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Should().BeOfType(typeof(ViewModel));
        }

        [TestMethod]
        public void Build_set_the_Id_property_correctly()
        {
            // Arrange
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public void Build_sets_the_ImportTypeId_property_correctly()
        {
            // Arrange
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportTypeId.Should().Be(2);
        }

        [TestMethod]
        public void Build_sets_the_ImportTypeName_property_correctly()
        {
            // Arrange
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportTypeName.Should().Be("Transaction Import Type Name");
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleId_property_correctly()
        {
            // Arrange
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleId.Should().Be(3);
        }

        [TestMethod]
        public void Build_sets_the_ImportProcessingRuleName_property_correctly()
        {
            // Arrange
            SetupService(_mockImportTypeImportProcessingRuleService);

            // Act
            var result = _viewModelBuilder.Build(1);

            // Assert
            result.ImportProcessingRuleName.Should().Be("Import Processing Rule Name");
        }
    }
}
