using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class CurrentStatusTests : TestBase
    {
        [TestMethod]
        [DataRow(ImportStatusEnum.Completed)]
        [DataRow(ImportStatusEnum.Failed)]
        [DataRow(ImportStatusEnum.Reverted)]
        public void CurrentStatus_OnAImport_ReturnsTheLastestStatusHistoryStatus(ImportStatusEnum latestStatus)
        {
            // Arrange
            var Import = GetImportWithImportStatusHistories(latestStatus);

            // Act
            var result = Import.CurrentStatus();

            // Assert
            result
                .Should()
                .Be(latestStatus);
        }
    }
}
