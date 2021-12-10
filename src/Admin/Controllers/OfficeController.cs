using Admin.Models.Office;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class OfficeController : BaseController<IOfficeControllerDependencies>
    {
        public OfficeController(IOfficeControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Offices")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Office Details")]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Office")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Office")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}