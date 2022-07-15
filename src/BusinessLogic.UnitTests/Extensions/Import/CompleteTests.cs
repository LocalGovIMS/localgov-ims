using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class CompleteTests : TestBase
    {
        private readonly List<string> Errors = new List<string>() { "Error", "Another Error" };
        private readonly List<string> NoErrors = new List<string>();

        [TestMethod]
        public void Complete_OnAImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Complete(Errors, TestUserId);

            // Assert
            Import.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Revert_OnAImportWithErrors_AddsAStatusHistoryWithAStatusOfFailed()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Complete(Errors, TestUserId);

            // Assert
            Import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.Failed);
        }

        [TestMethod]
        public void Revert_OnAImportWithNoErrors_AddsAStatusHistoryWithAStatusOfCompleted()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Complete(NoErrors, TestUserId);

            // Assert
            Import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.Completed);
        }
    }
}
