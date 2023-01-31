using Admin.Models.Import;
using BusinessLogic.Models.Import;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.FileImport.Confirm
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post : TestBase
    {
        public Post()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Confirm));
        }

        private ActionResult GetResult()
        {
            return Controller.Confirm(new ConfirmViewModel() { ImportId = 1, RowCount = 2 });
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
            MockProcessCommand.Setup(x => x.Execute(It.IsAny<int>()))
                .Returns(new Admin.Classes.Commands.CommandResult(false));

            // Act
            var result = GetResult();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToImportConfirmIfProcessImportCommandIsSuccessful()
        {
            // Arrange
            MockProcessCommand.Setup(x => x.Execute(It.IsAny<int>()))
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
            MockProcessCommand.Setup(x => x.Execute(It.IsAny<int>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true)
                {
                    Data = new ProcessResult()
                    {
                        FileImport = new BusinessLogic.Entities.FileImport()
                        {
                            ImportId = 1,
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
            MockProcessCommand.Setup(x => x.Execute(It.IsAny<int>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true)
                {
                    Data = new ProcessResult()
                    {
                        FileImport = new BusinessLogic.Entities.FileImport()
                        {
                            ImportId = 1,
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
