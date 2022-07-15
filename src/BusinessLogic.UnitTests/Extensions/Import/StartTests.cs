using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class StartTests : TestBase
    {
        [TestMethod]
        public void Start_OnAImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Start(TestUserId);

            // Assert
            Import.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Start_OnAImport_AddsAStatusHistoryWithAStatusOfInProgress()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Start(TestUserId);

            // Assert
            Import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.InProgress);
        }
    }
}
