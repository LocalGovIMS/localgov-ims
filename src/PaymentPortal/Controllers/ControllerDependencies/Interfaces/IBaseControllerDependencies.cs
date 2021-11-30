using log4net;

namespace PaymentPortal.Controllers
{
    public interface IBaseControllerDependencies
    {
        ILog Log { get; }
    }
}
