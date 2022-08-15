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
            var import = GetImport();

            import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Error
            });

            import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Error
            });

            return import;
        }

        protected Entities.Import GetImportWithInfo()
        {
            var import = GetImport();

            import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Info
            });

            import.EventLogs.Add(new Entities.ImportEventLog()
            {
                Type = (byte)ImportEventLogTypeEnum.Info
            });

            return import;
        }

        protected Entities.Import GetImportWithImportStatusHistories(ImportStatusEnum latestStatus)
        {
            var import = GetImport();

            import.StatusHistories.Add(new Entities.ImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now.AddMinutes(-1),
                StatusId = (int)ImportStatusEnum.Received
            });

            import.StatusHistories.Add(new Entities.ImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now,
                StatusId = (int)latestStatus
            });

            return import;
        }
    }
}
