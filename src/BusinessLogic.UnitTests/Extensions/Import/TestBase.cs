using BusinessLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class TestBase
    {
        protected const int TestUserId = 1;

        protected Entities.Import GetImport()
        {
            return new Entities.Import()
            {
                
            };
        }

        protected Entities.Import GetImportWithErrors()
        {
            var Import = GetImport();

            Import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Error
            });

            Import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Error
            });

            return Import;
        }

        protected Entities.Import GetImportWithInfo()
        {
            var Import = GetImport();

            Import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Info
            });

            Import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Info
            });

            return Import;
        }

        protected Entities.Import GetImportWithImportStatusHistories(ImportStatusEnum latestStatus)
        {
            var Import = GetImport();

            Import.StatusHistories.Add(new Entities.ImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now.AddMinutes(-1),
                StatusId = (int)ImportStatusEnum.Received
            });

            Import.StatusHistories.Add(new Entities.ImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now,
                StatusId = (int)latestStatus
            });

            return Import;
        }
    }
}
