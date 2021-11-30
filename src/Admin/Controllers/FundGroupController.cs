using Admin.Models.FundGroup;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]

    public class FundGroupController : BaseController<IFundGroupControllerDependencies>
    {
        public FundGroupController(IFundGroupControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Fund Groups")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Fund Group Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Fund Group")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand, "List");
        }

        [NavigatablePageActionFilter(DisplayText = "Create Fund Group")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = Dependencies.CreateViewModelBuilder.Build();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand, "List");
        }

        [NavigatablePageActionFilter(DisplayText = "Delete Fund Group")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteFundGroup(int id)
        {
            Dependencies.DeleteCommand.Execute(id);

            return RedirectToAction("List");
        }
    }
}