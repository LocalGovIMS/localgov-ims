using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.SuspenseNoteController;
using ControllerDependencies = Admin.Controllers.SuspenseNoteControllerDependencies;
using EditViewModel = Admin.Models.SuspenseNote.EditViewModel;
using ListViewModel = Admin.Models.SuspenseNote.ListViewModel;

namespace Admin.UnitTests.Controllers.SuspenseNote.ListForSuspense
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();

        private MethodInfo GetListMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "_ListForSuspense")
                .FirstOrDefault();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetListMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        [TestMethod]
        public void HasASingleGetAttribute()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        private ActionResult GetTestListResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<ListViewModel, int>>();
            listViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new ListViewModel());

            var dependencies = new ControllerDependencies(
                _mockLogger.Object,
                listViewModelBuilder.Object,
                _mockCreateCommand.Object);

            var controller = new Controller(dependencies);

            return controller._ListForSuspense(1);
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetTestListResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetTestListResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_List");
        }
    }
}
