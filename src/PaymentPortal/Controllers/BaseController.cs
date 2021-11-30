using System;
using System.Web.Mvc;

namespace PaymentPortal.Controllers
{
    public abstract class BaseController<T> : Controller where T : IBaseControllerDependencies
    {
        private T _dependencies;

        protected T Dependencies
        {
            get { return _dependencies; }
            private set { _dependencies = value; }
        }

        protected BaseController(T dependencies)
        {
            if (dependencies == null) throw new ArgumentNullException("dependencies");

            _dependencies = dependencies;
        }
    }
}