using log4net;
using System;

namespace PaymentPortal.Controllers
{
    public abstract class BaseControllerDependencies : IBaseControllerDependencies
    {
        protected BaseControllerDependencies(ILog log)
        {
            Log = log ?? throw new ArgumentNullException("log");
        }

        public ILog Log { get; private set; }
    }
}