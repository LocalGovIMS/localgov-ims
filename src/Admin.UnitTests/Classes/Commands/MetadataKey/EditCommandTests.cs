using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.MetadataKey.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.MetadataKey.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.MetadataKey
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMetadataKeyService> _mockMetadataKeyService = new Mock<IMetadataKeyService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Name = "Name",
                Description = "Description",
                IsASystemType = false,
                EntityType = BusinessLogic.Enums.MetadataKeyEntityType.Mop
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockMetadataKeyService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
