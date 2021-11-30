using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ImportProcessingRuleFieldService : BaseService, IImportProcessingRuleFieldService
    {
        public ImportProcessingRuleFieldService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
            : base(logger, unitOfWork, securityContext)
        {
        }

        public List<ImportProcessingRuleField> GetAll()
        {
            try
            {
                return UnitOfWork.ImportProcessingRuleFields.GetAll().ToList();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }
    }
}
