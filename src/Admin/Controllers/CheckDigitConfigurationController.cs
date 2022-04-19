using Admin.Classes.Commands.CheckDigitConfiguration;
using Admin.Models.CheckDigitConfiguration;
using Admin.Models.Shared;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class CheckDigitConfigurationController : BaseController<CheckDigitConfigurationControllerDependencies>
    {
        public CheckDigitConfigurationController(CheckDigitConfigurationControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Check Digit Configurations", OnlyComparePath = false)]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Check Digit Configurations", OnlyComparePath = false)]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Configuration Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Configuration")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            return BaseEdit(model, Dependencies.EditCommand);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Configuration")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = Dependencies.EditViewModelBuilder.Build();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            return BaseCreate(model, Dependencies.CreateCommand);
        }
    }
}