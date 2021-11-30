using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Template.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Template.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.Template
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITemplateService> _mockTemplateService = new Mock<ITemplateService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 0,
                AllowPdq = false,
                AllowCash = false,
                AllowCheque = false,
                Name = "Test"
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTemplateService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResultFromResult()
        {
            // Arrange
            _mockTemplateService.Setup(x => x.Update(It.IsAny<BusinessLogic.Entities.Template>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockTemplateService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
