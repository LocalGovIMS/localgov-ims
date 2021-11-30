using Admin.Interfaces.ModelBuilders;
using Admin.Models.SystemMessage;
using log4net;
using System;
using System.Collections.Generic;

namespace Admin.Controllers
{
    public class HomeControllerDependencies : BaseControllerDependencies, IHomeControllerDependencies
    {
        public HomeControllerDependencies(ILog log,
            IModelBuilder<IList<DetailsViewModel>, string> listViewModelBuilder)
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
        }

        #region Model Builders

        public IModelBuilder<IList<DetailsViewModel>, string> ListViewModelBuilder { get; private set; }

        #endregion
    }
}