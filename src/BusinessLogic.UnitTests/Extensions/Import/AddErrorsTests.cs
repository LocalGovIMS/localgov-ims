using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class AddErrorsTests : TestBase
    {
        [TestMethod]
        public void AddErrors_OnAnImport_CreatesTwoEventLogs()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.AddErrors(new List<string>() { "An error", "Another error" });

            // Assert
            import.EventLogs.Count()
                .Should()
                .Be(2);
        }

        [TestMethod]
        public void AddError_OnAnImport_CreatesAnEventLogsOfTheCorrectType()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.AddErrors(new List<string>() { "An error", "Another error" });

            // Assert
            import.EventLogs.All(x => x.Type == (byte)ImportEventLogTypeEnum.Error)
                .Should()
                .BeTrue();
        }
    }
}
