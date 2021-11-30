using Admin.Interfaces.ModelBuilders;
using Admin.Models.SystemMessage;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public interface IHomeControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<IList<DetailsViewModel>, string> ListViewModelBuilder { get; }
    }
}
