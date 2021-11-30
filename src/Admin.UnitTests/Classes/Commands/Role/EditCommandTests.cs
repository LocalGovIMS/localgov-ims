using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Role.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Role.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.Role
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                DisplayName = "Test Role",
                Name = "TestRole",
                Id = 0
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockRoleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult1()
        {
            // Arrange
            _mockRoleService.Setup(
                x => x.Update(It.IsAny<BusinessLogic.Entities.Role>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockRoleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
