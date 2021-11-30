using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportProcessingRuleFieldService
    {
        List<ImportProcessingRuleField> GetAll();
    }
}
