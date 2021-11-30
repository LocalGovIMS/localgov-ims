using Admin.Models.Shared;
using Admin.Models.Template;
using BusinessLogic.Security;
using System.Linq;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class TemplateController : BaseController<ITemplateControllerDependencies>
    {
        public TemplateController(ITemplateControllerDependencies dependencies) : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Templates")]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View(model);
        }

        [NavigatablePageActionFilter(DisplayText = "Create Template")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = Dependencies.CreateCommand.Execute(model);

            // TODO: This message never gets read - we just redirect off to 'Back' regardless of the result
            model.Message = result.Success
                ? new SuccessMessage("Your template was created", "Template created") as Message
                : new ErrorMessage(result.Messages.First(), "Error");

            return RedirectToAction("Back");
        }

        [NavigatablePageActionFilter(DisplayText = "Edit Template")]
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
                model.Message = new ErrorMessage("There are errors in the data you've submitted", "Error");
                return View(Dependencies.EditViewModelBuilder.Rebuild(model));
            }

            var result = Dependencies.EditCommand.Execute(model);

            model.Message = result.Success
                ? new SuccessMessage("Your changes were saved", "Template updated")
                : model.Message = new ErrorMessage(result.Messages.First(), "Error");

            model = Dependencies.EditViewModelBuilder.Rebuild(model);

            return View(model);
        }
    }
}