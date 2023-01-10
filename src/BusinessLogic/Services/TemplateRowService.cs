//using BusinessLogic.Entities;
//using BusinessLogic.Interfaces.Persistence;
//using BusinessLogic.Interfaces.Security;
//using BusinessLogic.Interfaces.Services;
//using log4net;
//using System;

//namespace BusinessLogic.Services
//{
//    public class TemplateRowService : BaseService, ITemplateRowService
//    {
//        public TemplateRowService(ILog logger
//            , IUnitOfWork unitOfWork
//            , ISecurityContext securityContext)
//            : base(logger, unitOfWork, securityContext)
//        {
//        }

//        public TemplateRow GetTemplateRow(int id)
//        {
//            try
//            {
//                var result = UnitOfWork.TemplateRows.GetTemplateRow(id);
//                return result;
//            }
//            catch (Exception e)
//            {
//                Logger.Error(null, e);
//                return null;
//            }
//        }
//    }
//}
