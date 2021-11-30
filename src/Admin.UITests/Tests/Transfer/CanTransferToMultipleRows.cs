using Admin.UITests.PageObjects;
using Admin.UITests.PageObjects.Transaction;
using Admin.UITests.PageObjects.Transfer;
using BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Admin.UITests.Tests.Transfer
{
    [TestClass]
    [TestCategory("Transfer")]
    [ExcludeFromCodeCoverage]
    public class CanTransferToMultipleRows : TestBase
    {
        [TestMethod("CanTransferToMultipleRows")]
        public void Test()
        {
            var menu = new MainMenu(WebDriver, BaseUrl);
            menu.ClickTransfersLink();

            var transferPage = new Index(WebDriver);

            AddTransferItems(transferPage);

            transferPage.SubmitTransfer();

            Thread.Sleep(200);

            var transactionPage = new Details(WebDriver);

            Thread.Sleep(200);

            Assert.AreEqual("Transfer In (10)", transactionPage.MethodOfPaymentElement.Text);
            Assert.AreEqual("£10.00", transactionPage.TotalPaymentElement.Text);
            Assert.AreEqual(2, transactionPage.PaymentCount());
        }

        private void AddTransferItems(Index transferPage)
        {
            var item = new TransferItem
            {
                AccountReference = "12345678901",
                Amount = (decimal)10.00,
                VatCode = "W0",
                FundCode = "13"
            };

            var targetItem1 = new TransferItem
            {
                AccountReference = "12345678904",
                Amount = (decimal)3.34,
                VatCode = "W0",
                FundCode = "13"
            };

            var targetItem2 = new TransferItem
            {
                AccountReference = "12345678905",
                Amount = (decimal)6.66,
                VatCode = "W0",
                FundCode = "13"
            };

            transferPage.AddSourceItem(item);
            transferPage.AddTargetItem(targetItem1);
            transferPage.AddTargetItem(targetItem2);
        }
    }
}
