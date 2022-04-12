using Admin.Models.Fund;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class FundController : BaseController<IFundControllerDependencies>
    {
        public FundController(IFundControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Fund Codes")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Fund Codes")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Fund Code Details")]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Fund Code")]
        [HttpGet]
        public ActionResult Edit(string id)
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

        [NavigatablePageActionFilter(DisplayText = "Create Fund Code")]
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