using log4net;
using System;

namespace Admin.Controllers
{
    public abstract class BaseControllerDependencies : IBaseControllerDependencies
    {
        public BaseControllerDependencies(ILog log)
        {
            Log = log ?? throw new ArgumentNullException("log");
        }

        public ILog Log { get; private set; }
    }
}