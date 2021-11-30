using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Template;
using log4net;
using Unity;

namespace Admin.Controllers
{
    public class TemplateControllerDependencies : BaseControllerDependencies, ITemplateControllerDependencies
    {
        public TemplateControllerDependencies(ILog log,
            IModelBuilder<ListViewModel, int> listViewModelBuilder,
            IModelBuilder<EditViewModel, int> editViewModelBuilder,
            [Dependency("Create")] IModelCommand<EditViewModel> createCommand,
            [Dependency("Edit")] IModelCommand<EditViewModel> editCommand)
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder;
            EditViewModelBuilder = editViewModelBuilder;
            CreateCommand = createCommand;
            EditCommand = editCommand;
        }

        public IModelBuilder<ListViewModel, int> ListViewModelBuilder { get; private set; }
        public IModelBuilder<EditViewModel, int> EditViewModelBuilder { get; private set; }
        public IModelCommand<EditViewModel> CreateCommand { get; private set; }
        public IModelCommand<EditViewModel> EditCommand { get; private set; }
    }
}