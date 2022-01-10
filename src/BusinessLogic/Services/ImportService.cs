using BusinessLogic.Classes.Result;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace BusinessLogic.Services
{
    public class ImportService : BaseService, IImportService
    {
        private readonly IFileImporter _fileImporter;
        private readonly IImportProcessor _importProcessor;

        public ImportService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IFileImporter fileImporter
            , IImportProcessor importProcessor)
            : base(logger, unitOfWork, securityContext)
        {
            _fileImporter = fileImporter ?? throw new ArgumentNullException("fileImporter");
            _importProcessor = importProcessor ?? throw new ArgumentNullException("importProcessor");
        }

        public IResult LoadFromFile(string path)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to import transactions");

            try
            {
                return _fileImporter.ImportFile(new FileImporterArgs()
                {
                    BatchReference = GetNextReferenceId(),
                    Path = path
                });
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return new Result("Unable to import the transactions");
            }
        }

        public IResult Process(string batchReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to import transactions");

            try
            {
                return _importProcessor.Process(new ImportProcessorArgs()
                {
                    BatchReference = batchReference,
                });
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return new Result("Unable to import the transactions");
            }
        }
    }
}
