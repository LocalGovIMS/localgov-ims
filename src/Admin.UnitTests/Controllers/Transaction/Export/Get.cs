using Admin.Classes.Commands.Transaction;
using Admin.Models.Transaction;
using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PagedList;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.TransactionController;

namespace Admin.UnitTests.Controllers.Transaction.Export
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return typeof(Controller).GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Export")
                .FirstOrDefault();
        }

        private ActionResult GetResult(bool success)
        {
            MockListViewModelBuilder.Setup(x => x.Build(
                It.IsAny<SearchCriteria>()))
                .Returns(new ListViewModel() { Transactions = new StaticPagedList<ProcessedTransaction>(new List<ProcessedTransaction>(), 1, 1, 1) });

            MockCreateCsvFileForExportCommand.Setup(x => x.Execute(
                It.IsAny<CreateCsvFileForExportCommandArgs>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success, "TestMessage") { Data = new FileContentResult(new byte[0], "Text/csv") });

            return Controller.Export(new Models.Transaction.SearchCriteria());
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void NavigatablePageActionFilterAttributeHasCorrectDisplayText()
        {
            var attribute = GetMethod().CustomAttributes.Single(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute));

            var namedArgument = attribute.NamedArguments.Where(x => x.MemberName == "DisplayText").First();

            Assert.AreEqual("Export", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAFileContentResult()
        {
            var result = GetResult(true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }
    }
}
