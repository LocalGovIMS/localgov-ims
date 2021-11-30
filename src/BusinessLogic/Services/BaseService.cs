using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using HashidsNet;
using log4net;
using System;
using System.Configuration;

namespace BusinessLogic.Services
{
    public abstract class BaseService
    {
        protected readonly ILog Logger;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ISecurityContext SecurityContext;
        private readonly Random _randomGenerator;

        public BaseService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext)
        {
            Logger = logger ?? throw new ArgumentNullException("logger");
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            SecurityContext = securityContext ?? throw new ArgumentNullException("securityContext");
            _randomGenerator = new Random();
        }

        public string GetNextReferenceId()
        {
            var hash = new Hashids(ConfigurationManager.AppSettings["ReferenceSalt"], 9);
            return hash.Encode(DateTime.Now.Day
                + DateTime.Now.Month
                + DateTime.Now.Year
                + DateTime.Now.Hour
                + DateTime.Now.Minute
                + DateTime.Now.Second
                + DateTime.Now.Millisecond
                , _randomGenerator.Next(9999999));
        }
    }
}
