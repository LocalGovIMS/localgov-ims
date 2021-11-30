using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Web.Mvc;
using Command = Admin.Classes.Commands.FundGroup.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.FundGroup.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.FundGroup
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundGroupService> _mockFundGroupService = new Mock<IFundGroupService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 0,
                FundGroupName = "New Fund Group",
                Funds = new List<CheckBoxListItem>()
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
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockFundGroupService.Object);

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
            _mockFundGroupService.Setup(x => x.Create(It.IsAny<BusinessLogic.Entities.FundGroup>()))
               .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockFundGroupService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
