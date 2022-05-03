using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Web.Mvc;
using Command = Admin.Classes.Commands.UserMethodOfPayment.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.UserMethodOfPayment.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.UserMethodOfPayment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserMethodOfPaymentService> _mockUserMethodOfPaymentService = new Mock<IUserMethodOfPaymentService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                MopCodes = new List<CheckBoxListItem>()
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
                _mockUserMethodOfPaymentService.Object);

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
            _mockUserMethodOfPaymentService.Setup(x => x.Update(
                It.IsAny<List<BusinessLogic.Entities.UserMethodOfPayment>>(),
                It.IsAny<int>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockUserMethodOfPaymentService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
