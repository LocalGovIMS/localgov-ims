using Admin.Classes.Commands.Import;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Import;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.ImportController;
using Dependencies = Admin.Controllers.ImportControllerDependencies;

namespace Admin.UnitTests.Controllers.Import.Confirm
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<SaveImportFileCommandArgs>> _mockSaveImportFileCommand = new Mock<IModelCommand<SaveImportFileCommandArgs>>();
        private readonly Mock<IModelCommand<string>> _mockProcessImportCommand = new Mock<IModelCommand<string>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Confirm")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockSaveImportFileCommand.Object,
                _mockProcessImportCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Confirm(new ConfirmViewModel() { BatchReference = "Batch Reference", RowCount = 2 });
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfProcessImportCommandFails()
        {
            // Arrange
            _mockProcessImportCommand.Setup(x => x.Execute(It.IsAny<string>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            // Act
            var result = GetResult();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToImportConfirmIfProcessImportCommandIsSuccessful()
        {
            // Arrange
            _mockProcessImportCommand.Setup(x => x.Execute(It.IsAny<string>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            // Act
            var result = GetResult() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Confirm");
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfProcessImportCommandIsSuccessful()
        {
            // Arrange
            _mockProcessImportCommand.Setup(x => x.Execute(It.IsAny<string>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true)
                {
                    Data = new ProcessResult()
                    {
                        Import = new BusinessLogic.Entities.Import()
                        {
                            BatchReference = "Batch Reference",
                        },
                        NumberOfRowsImported = 2
                    }
                });

            // Act
            var result = GetResult();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToTransactionSearchIfProcessImportCommandIsSuccessful()
        {
            // Arrange
            _mockProcessImportCommand.Setup(x => x.Execute(It.IsAny<string>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true)
                {
                    Data = new ProcessResult()
                    {
                        Import = new BusinessLogic.Entities.Import()
                        {
                            BatchReference = "Batch Reference",
                        },
                        NumberOfRowsImported = 2
                    }
                });

            // Act
            var result = GetResult() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(result.RouteValues["controller"], "Transaction");
            Assert.AreEqual(result.RouteValues["action"], "Search");
        }
    }
}
