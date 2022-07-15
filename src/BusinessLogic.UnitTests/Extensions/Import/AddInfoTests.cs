using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class AddInfoTests : TestBase
    {
        [TestMethod]
        public void AddInfo_OnATransaction_CreatesAnEventLog()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.AddInfo("An error");

            // Assert
            Import.EventLogs.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AddInfo_OnATransaction_CreatesAnEventLogOfTheCorrectType()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.AddInfo("An error");

            // Assert
            Import.EventLogs.First().Type
                .Should()
                .Be((byte)ImportEventLogTypeEnum.Info);
        }
    }
}
