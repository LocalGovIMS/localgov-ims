using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Web.Mvc;
using Command = Admin.Classes.Commands.UserFundGroup.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.UserFundGroup.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.UserFundGroup
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IUserFundGroupService> _mockUserFundGroupService = new Mock<IUserFundGroupService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                FundGroups = new List<CheckBoxListItem>()
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
                _mockUserFundGroupService.Object);

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
            _mockUserFundGroupService.Setup(x => x.Update(
                It.IsAny<List<BusinessLogic.Entities.UserFundGroup>>(),
                It.IsAny<int>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockUserFundGroupService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
