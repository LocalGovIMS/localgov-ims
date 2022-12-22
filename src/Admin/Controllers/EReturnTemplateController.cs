using Admin.Models.EReturnTemplate;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class EReturnTemplateController : BaseController<IEReturnTemplateControllerDependencies>
    {
        public EReturnTemplateController(IEReturnTemplateControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "eReturn Templates")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "eReturn Templates")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "eReturn Template Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit eReturn Template")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Dependencies.EditViewModelBuilder.Rebuild(model);

                return View(model);
            }

            Dependencies.EditCommand.Execute(model);

            return RedirectToAction("Back");
        }

        [NavigatablePageActionFilter(DisplayText = "Create eReturn Template")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();
            model = Dependencies.EditViewModelBuilder.Rebuild(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Dependencies.EditViewModelBuilder.Rebuild(model);

                return View(model);
            }

            Dependencies.CreateCommand.Execute(model);

            return RedirectToAction("Back");
        }
    }
}