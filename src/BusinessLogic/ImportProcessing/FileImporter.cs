using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Models;
using log4net;
using System;
using System.IO.Abstractions;

namespace BusinessLogic.ImportProcessing
{
    public class FileImporter : IFileImporter
    {
        private readonly ILog _log;
        private readonly ISecurityContext _securityContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileSystem _fileSystem;

        private int _rowCount = 0;
        private Import _import;

        public FileImporter(ILog log
            , ISecurityContext securityContext
            , IUnitOfWork unitOfWork
            , IFileSystem fileSystem)
        {
            _log = log ?? throw new ArgumentNullException("log");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _fileSystem = fileSystem ?? throw new ArgumentNullException("fileSystem");
        }

        public IResult ImportFile(FileImporterArgs args)
        {
            CreateImport(args);

            CreateImportRows(args);

            SaveImport();

            return CreateResult();
        }

        private void CreateImport(FileImporterArgs args)
        {
            _import = new Import()
            {
                BatchReference = args.BatchReference,
                CreatedAt = DateTime.Now,
                CreatedByUserId = _securityContext.UserId,
                OriginalFilename = args.Path, // TODO: Sort this out...
                WorkingFilename = args.Path // TODO: Sort this out...
            };
        }

        private void CreateImportRows(FileImporterArgs args)
        {
            using (var sr = _fileSystem.File.OpenText(args.Path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    _rowCount++;

                    _import.Rows.Add(new ImportRow
                    {
                        RowData = line
                    });
                }
            }
        }

        private void SaveImport()
        {
            _unitOfWork.Imports.Add(_import);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            result.SetData(new LoadFromFileResult()
            {
                Import = _import,
                RowCount = _rowCount
            });

            return result;
        }
    }

    public class FileImporterArgs
    {
        public string Path { get; set; }
        public string BatchReference { get; set; }
    }
}
