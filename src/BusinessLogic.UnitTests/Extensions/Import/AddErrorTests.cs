using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class AddErrorTests : TestBase
    {
        [TestMethod]
        public void AddError_OnAnImport_CreatesAnEventLog()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.AddError("An error");

            // Assert
            import.EventLogs.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AddError_OnAnImport_CreatesAnEventLogOfTheCorrectType()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.AddError("An error");

            // Assert
            import.EventLogs.First().Type
                .Should()
                .Be((byte)ImportEventLogTypeEnum.Error);
        }
    }
}
