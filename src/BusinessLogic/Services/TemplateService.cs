//using BusinessLogic.Classes.Result;
//using BusinessLogic.Entities;
//using BusinessLogic.Interfaces.Persistence;
//using BusinessLogic.Interfaces.Result;
//using BusinessLogic.Interfaces.Security;
//using BusinessLogic.Interfaces.Services;
//using BusinessLogic.Interfaces.Validators;
//using log4net;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace BusinessLogic.Services
//{
//    public class TemplateService : BaseService, ITemplateService
//    {
//        private ITemplateRowValidator _templateRowValidator;

//        public TemplateService(ILog logger
//            , IUnitOfWork unitOfWork
//            , ISecurityContext securityContext
//            , ITemplateRowValidator templateRowValidator)
//            : base(logger, unitOfWork, securityContext)
//        {
//            _templateRowValidator = templateRowValidator;
//        }

//        public List<Template> GetAllTemplates()
//        {
//            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
//                && !SecurityContext.IsInRole(Security.Role.EReturns)
//                && !SecurityContext.IsInRole(Security.Role.ServiceDesk)
//                && !SecurityContext.IsInRole(Security.Role.EReturnAuthoriser)) return new List<Template>();

//            try
//            {
//                return UnitOfWork.Templates.GetAll().ToList();
//            }
//            catch (Exception e)
//            {
//                Logger.Error(null, e);
//                return new List<Template>();
//            }
//        }

//        public Template GetTemplate(int id)
//        {
//            if (!SecurityContext.IsInRole(Security.Role.SystemAdmin)
//                && !SecurityContext.IsInRole(Security.Role.EReturns)) return null;

//            try
//            {
//                var result = UnitOfWork.Templates.GetTemplate(id);
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
