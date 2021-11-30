using Admin.Models.Vat;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class VatController : BaseController<IVatControllerDependencies>
    {
        public VatController(IVatControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "VAT Codes")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "VAT Code Details")]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit VAT Code")]
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

        [NavigatablePageActionFilter(DisplayText = "Create VAT Code")]
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