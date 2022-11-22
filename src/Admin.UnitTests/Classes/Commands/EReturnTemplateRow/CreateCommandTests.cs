using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.EReturnTemplateRow.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.EReturnTemplateRow.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.EReturnTemplateRow
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateRowService> _mockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Reference = "Test reference",
                ReferenceOverride = false,
                Description = "Test description",
                DescriptionOverride = true,
                VatCode = "V1",
                VatOverride = false
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Create_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockEReturnTemplateRowService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.TemplateRow>()), Times.Once);
        }
    }
}
