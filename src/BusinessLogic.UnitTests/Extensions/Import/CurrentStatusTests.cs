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
        [DataRow(ImportStatusEnum.Succeeded)]
        [DataRow(ImportStatusEnum.Failed)]
        [DataRow(ImportStatusEnum.Reverted)]
        public void CurrentStatus_OnAImport_ReturnsTheLastestStatusHistoryStatus(ImportStatusEnum latestStatus)
        {
            // Arrange
            var import = GetImportWithImportStatusHistories(latestStatus);

            // Act
            var result = import.CurrentStatus();

            // Assert
            result
                .Should()
                .Be(latestStatus);
        }
    }
}
