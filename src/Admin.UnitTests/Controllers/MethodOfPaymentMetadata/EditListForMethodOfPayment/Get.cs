﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using ListViewModel = Admin.Models.MethodOfPaymentMetadata.ListViewModel;
using SearchCriteria = Admin.Models.MethodOfPaymentMetadata.SearchCriteria;

namespace Admin.UnitTests.Controllers.MethodOfPaymentMetadata.EditListForMethodOfPayment
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
            return GetMethod(typeof(ChildActionOnlyAttribute), nameof(Controller._EditListForMethodOfPayment));
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        private ActionResult GetTestListResult()
        {
            MockListViewModelBuilder.Setup(x => x.Build(It.IsAny<SearchCriteria>())).Returns(new ListViewModel());

            return Controller._EditListForMethodOfPayment("1");
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_EditList");
        }
    }
}
