using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Web.Mvc;
using Command = Admin.Classes.Commands.UserRole.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.UserRole.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.UserRole
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserRoleService> _mockUserRoleService = new Mock<IUserRoleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Roles = new List<CheckBoxListItem>()
                {
                    {
                        new CheckBoxListItem()
                        {
                            Id = "0",
                            IsChecked = false,
                            Text = "Item 1"
                        }
                    },
                    {
                        new CheckBoxListItem()
                        {
                            Id = "1",
                            IsChecked = true,
                            Text = "Item 2"
                        }
                    }
                },
                UserId = 0,
                UserName = string.Empty
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockUserRoleService.Object);

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
            _mockUserRoleService.Setup(x => x.Update(
                It.IsAny<List<BusinessLogic.Entities.UserRole>>(),
                It.IsAny<int>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockUserRoleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
