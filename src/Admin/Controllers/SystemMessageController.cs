using Admin.Models.SystemMessage;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class SystemMessageController : BaseController<ISystemMessageControllerDependencies>
    {
        public SystemMessageController(ISystemMessageControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "System Messages")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Edit System Message")]
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

        [NavigatablePageActionFilter(DisplayText = "Create System Message")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = Dependencies.EditViewModelBuilder.Build();

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