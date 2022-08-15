using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class RevertTests : TestBase
    {
        [TestMethod]
        public void Revert_OnAImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.Revert(TestUserId);

            // Assert
            import.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Revert_OnAImport_AddsAStatusHistoryWithAStatusOfReverted()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.Revert(TestUserId);

            // Assert
            import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.Reverted);
        }
    }
}
