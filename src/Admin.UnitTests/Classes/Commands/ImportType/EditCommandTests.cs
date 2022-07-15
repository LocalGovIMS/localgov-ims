using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportType.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportType.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportType
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportTypeService> _mockImportTypeService = new Mock<IImportTypeService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportTypeService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportTypeService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportTypeService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockImportTypeService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.ImportType>()), Times.Once);
        }
    }
}
