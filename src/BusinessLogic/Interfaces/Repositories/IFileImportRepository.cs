﻿using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFileImportRepository : IRepository<FileImport>
    {
        FileImport GetByBatchReference(string batchReference);
    }
}