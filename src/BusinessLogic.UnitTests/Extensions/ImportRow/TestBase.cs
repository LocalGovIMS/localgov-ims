using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.ImportRow
{
    [TestClass]
    public class TestBase
    {
        protected const int TestImportId = 1;

        protected List<Entities.ImportRow> GetImportRows()
        {
            return new List<Entities.ImportRow>()
            {
                new Entities.ImportRow()
                {
                    Id = 1
                },
                new Entities.ImportRow()
                {
                    Id = 2
                }
            };
        }
    }
}
