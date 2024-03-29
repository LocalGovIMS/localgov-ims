﻿using BusinessLogic.Enums;
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
            var import = GetImport();

            // Act
            import.AddInfo("An error");

            // Assert
            import.EventLogs.Count()
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AddInfo_OnATransaction_CreatesAnEventLogOfTheCorrectType()
        {
            // Arrange
            var import = GetImport();

            // Act
            import.AddInfo("An error");

            // Assert
            import.EventLogs.First().Type
                .Should()
                .Be((byte)ImportEventLogTypeEnum.Info);
        }
    }
}
