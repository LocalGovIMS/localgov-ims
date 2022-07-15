using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class InitialiseTests : TestBase
    {
        [TestMethod]
        public void Initialise_OnAImport_AddsAStatusHistoryEntry()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Initialise(TestUserId);

            // Assert
            Import.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Initialise_OnAImport_AddsAStatusHistoryWithAStatusOfReceived()
        {
            // Arrange
            var Import = GetImport();

            // Act
            Import.Initialise(TestUserId);

            // Assert
            Import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.Received);
        }
    }
}
