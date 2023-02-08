using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.EReturn.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.EReturn.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.EReturn
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnService> _mockEReturnService = new Mock<IEReturnService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                EReturn = new BusinessLogic.Models.EReturnWrapper()
                {
                    EReturn = new BusinessLogic.Entities.EReturn()
                    {
                        EReturnCashes = new List<BusinessLogic.Entities.EReturnCash>(),
                        EReturnCheques = new List<BusinessLogic.Entities.EReturnCheque>()
                    }
                }
            };
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
                e.ParamName.Should().Be("eReturnService");
            }
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnService.Object);

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
            _mockEReturnService.Setup(x => x.Update(It.IsAny<BusinessLogic.Entities.EReturn>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockEReturnService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
