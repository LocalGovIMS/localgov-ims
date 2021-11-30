using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportProcessingRuleOperatorService
    {
        List<ImportProcessingRuleOperator> GetAll();
    }
}
