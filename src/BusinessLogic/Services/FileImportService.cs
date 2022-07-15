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
    public class FileImportService : BaseService, IFileImportService
    {
        private readonly IFileImporter _fileImporter;
        private readonly IFileImportProcessor _fileImportProcessor;

        public FileImportService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IFileImporter fileImporter
            , IFileImportProcessor fileImportProcessor)
            : base(logger, unitOfWork, securityContext)
        {
            _fileImporter = fileImporter ?? throw new ArgumentNullException("fileImporter");
            _fileImportProcessor = fileImportProcessor ?? throw new ArgumentNullException("fileImportProcessor");
        }

        public IResult LoadFromFile(string path)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to import transactions");

            try
            {
                return _fileImporter.ImportFile(new FileImporterArgs()
                {
                    Path = path,
                    ImportTypeId = 1 // TODO: This needs to come from the UI
                });
            }
            catch (Exception e)
            {
                Logger.Error(null, e);

                return new Result("Unable to import the transactions");
            }
        }

        public IResult Process(int importId)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to import transactions");

            try
            {
                return _fileImportProcessor.Process(new FileImportProcessorArgs()
                {
                    ImportId = importId,
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
