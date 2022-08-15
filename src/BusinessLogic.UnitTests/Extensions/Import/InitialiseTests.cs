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
            var import = GetImport();

            // Act
            import.Initialise(TestUserId);

            // Assert
            import.StatusHistories.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void Initialise_OnAImport_AddsAStatusHistoryWithAStatusOfReceived()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.Initialise(TestUserId);

            // Assert
            import.StatusHistories.First().StatusId
                .Should()
                .Be((int)ImportStatusEnum.Received);
        }
    }
}
