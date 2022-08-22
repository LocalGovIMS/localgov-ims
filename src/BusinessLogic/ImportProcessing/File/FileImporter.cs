using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Models.Import;
using log4net;
using System;
using System.Collections.Generic;
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
        private FileImport _fileImport;

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

            SaveImport();

            return CreateResult();
        }

        private void CreateImport(FileImporterArgs args)
        {
            var rows = CreateImportRows(args);

            _fileImport = new FileImport()
            {
                Import = new Import()
                {
                    ImportTypeId = args.ImportTypeId,
                    Notes = string.Empty,
                    CreatedDate = DateTime.Now,
                    CreatedByUserId = _securityContext.UserId,
                    Rows = rows,
                    NumberOfRows = rows.Count
                },
                OriginalFilename = args.Path, // TODO: Sort this out...
                WorkingFilename = args.Path // TODO: Sort this out...
            };

            _fileImport.Import.Initialise(_securityContext.UserId);
        }

        private List<ImportRow> CreateImportRows(FileImporterArgs args)
        {
            var importRows = new List<ImportRow>();

            using (var sr = _fileSystem.File.OpenText(args.Path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    _rowCount++;

                    importRows.Add(new ImportRow
                    {
                        Data = line
                    });
                }
            }

            return importRows;
        }

        private void SaveImport()
        {
            _unitOfWork.FileImports.Add(_fileImport);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private IResult CreateResult()
        {
            var result = new Result();

            result.SetData(new LoadFromFileResult()
            {
                FileImport = _fileImport,
                RowCount = _rowCount
            });

            return result;
        }
    }

    public class FileImporterArgs
    {
        public string Path { get; set; }
        public int ImportTypeId { get; set; }
    }
}
