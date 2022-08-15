using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.ImportRow
{
    [TestClass]
    public class UpdateImportIdTests : TestBase
    {
        [TestMethod]
        public void UpdateImportId_OnAnListOfImportRows_UpdatesTheImportIdOfAllTheEntries()
        {
            // Arrange
            var importRows = GetImportRows();

            // Act
            importRows.UpdateImportId(TestImportId);

            // Assert
            importRows.All(x => x.ImportId == TestImportId)
                .Should()
                .BeTrue();
        }
    }
}
