using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.AccountHolder.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.AccountHolder.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                FundCode = "F1"
            };
        }

        private void SetupAccountHolderService()
        {
            _mockAccountHolderService.Setup(x => x.Update(It.IsAny<BusinessLogic.Entities.AccountHolder>()))
                .Returns(new Result()
                {
                    Data = new BusinessLogic.Entities.AccountHolder()
                    {
                        AccountReference = "AccountReference"
                    }
                });
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenAccountHolderServiceIsNull()
        {
            try
            {
                var command = new Command(
                    _mockLogger.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionParamNameIfDependenciesIsNull()
        {
            try
            {
                var command = new Command(
                    _mockLogger.Object,
                    null);
            }
            catch (ArgumentNullException e)
            {
                e.ParamName.Should().Be("accountHolderService");
            }
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupAccountHolderService();

            var command = new Command(
                _mockLogger.Object,
                _mockAccountHolderService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
