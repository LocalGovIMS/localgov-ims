using Admin.UITests.PageObjects;
using Admin.UITests.PageObjects.Transfer;
using BusinessLogic.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.Tests.Transfer
{
    [TestClass]
    [TestCategory("Transfer")]
    [ExcludeFromCodeCoverage]
    public class CannotTransferWithMissingSourceInfo : TestBase
    {
        [TestMethod("CannotTransferWithMissingSourceInfo")]
        public void Test()
        {
            var menu = new MainMenu(WebDriver, BaseUrl);
            menu.ClickTransfersLink();

            var transferPage = new Index(WebDriver);

            AddTransferItems(transferPage);

            transferPage.IsTransferEnabled().Should().BeFalse();
        }

        private void AddTransferItems(Index transferPage)
        {
            var targetItem1 = new TransferItem
            {
                AccountReference = "12345678904",
                Amount = (decimal)10.01,
                VatCode = "W0",
                FundCode = "13"
            };

            transferPage.AddTargetItem(targetItem1);
        }
    }
}
