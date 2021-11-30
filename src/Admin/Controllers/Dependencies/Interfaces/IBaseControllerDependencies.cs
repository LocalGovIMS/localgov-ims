using log4net;

namespace Admin.Controllers
{
    public interface IBaseControllerDependencies
    {
        ILog Log { get; }
    }
}
