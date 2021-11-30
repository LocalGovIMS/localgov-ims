using Admin.Controllers;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.SystemMessage;
using Admin.UnitTests.Helpers;
using BusinessLogic.Interfaces.Security;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.HomeController;

namespace Admin.UnitTests.Controllers.Home.Index
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IDependencyResolver> _mockDependencyResolver = new Mock<IDependencyResolver>();
        private readonly Mock<IUserStore> _mockUserStore = new Mock<IUserStore>();
        private readonly Mock<HttpContextBase> _mockControllerContextBase = new Mock<HttpContextBase>();
        private readonly Mock<IModelBuilder<IList<DetailsViewModel>, string>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<DetailsViewModel>, string>>();

        private void Setup(bool includeRoles)
        {
            _mockUserStore.Setup(x => x.GetUserRoles(
                It.IsAny<string>()))
                .Returns(includeRoles ? new string[] { "1", "2" } : new string[] { });

            _mockDependencyResolver.Setup(m => m.GetService(It.IsAny<Type>())).Returns(_mockUserStore.Object);

            DependencyResolver.SetResolver(_mockDependencyResolver.Object);
        }

        private void SetupHttpContext()
        {
            HttpContext.Current = MoqHelper.FakeHttpContext();

            var fakeIdentity = new GenericIdentity("User");

            var principal = new GenericPrincipal(fakeIdentity, new[]
            {
                BusinessLogic.Security.Role.Dashboard,
                BusinessLogic.Security.Role.Payments
            });

            HttpContext.Current.User = principal;
        }

        private void SetupControllerContextBase()
        {
            _mockControllerContextBase.Setup(x => x.User).Returns(HttpContext.Current.User);
        }

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Index")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var dependencies = new HomeControllerDependencies(_mockLogger.Object, _mockListViewModelBuilder.Object);

            var controller = new Controller(dependencies);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = _mockControllerContextBase.Object
            };

            return controller.Index();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
        {
            Setup(true);
            SetupHttpContext();
            SetupControllerContextBase();

            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            Setup(true);
            SetupHttpContext();
            SetupControllerContextBase();

            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void ReturnsRedirectResult()
        {
            Setup(false);
            SetupHttpContext();
            SetupControllerContextBase();

            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToSearch()
        {
            Setup(false);
            SetupHttpContext();
            SetupControllerContextBase();

            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.AreEqual("Unauthorised", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

    }
}
