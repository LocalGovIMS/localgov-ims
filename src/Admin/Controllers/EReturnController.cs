using Admin.Models.EReturn;
using Admin.Models.Shared;
using BusinessLogic.Entities;
using System.Web.Mvc;
using Web.Mvc;
using Web.Mvc.Navigation;
using Role = BusinessLogic.Security.Role;

namespace Admin.Controllers
{
    public class EReturnController : BaseController<IEReturnControllerDependencies>
    {

        public EReturnController(IEReturnControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "eReturns")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            if (TempData["Message"] != null)
                model.Message = TempData["Message"] as Message;

            return View("List", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "eReturns")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            if (TempData["Message"] != null)
                model.Message = TempData["Message"] as Message;

            return View("List", model);
        }


        [NavigatablePageActionFilter(DisplayText = "Approve eReturn")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturnAuthoriser)]
        [HttpGet]
        public ActionResult Approve(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            if (model == null)
            {
                TempData["message"] = new ErrorMessage("Unable to approve the selected eReturn", "Error");
                return RedirectToAction("Search");
            }

            return View(model);
        }

        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Approve")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturnAuthoriser)]
        [HttpPost]
        public ActionResult Approve(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = new ErrorMessage("An error occurred while saving");
                return View(Dependencies.EditViewModelBuilder.Rebuild(model));
            }

            Dependencies.ApproverEditCommand.Execute(model);

            var result = Dependencies.EditCommand.Execute(model);

            if (result.Success)
            {
                return RedirectToAction("Back");
            }

            model.Message = new ErrorMessage(result.Messages, "Save Failed");
            return View(Dependencies.EditViewModelBuilder.Rebuild(model));
        }

        [NavigatablePageActionFilter(DisplayText = "Edit eReturn")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Dependencies.EditViewModelBuilder.Build(id);

            if (model == null)
            {
                TempData["message"] = new ErrorMessage("Unable to edit the selected eReturn", "Error");
                return RedirectToAction("Search");
            }

            return View(model);
        }

        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Edit")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser)]
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = new ErrorMessage("An error occurred while saving");
                return View(Dependencies.EditViewModelBuilder.Rebuild(model));
            }

            var result = Dependencies.EditCommand.Execute(model);

            if (result.Success)
            {
                model = Dependencies.EditViewModelBuilder.Build(model.EReturn.EReturn.Id);
                model.Message = new SuccessMessage("Your changes have been saved", "Save successful");
                return View(model);
            }
            else
            {
                model.Message = new ErrorMessage(result.Messages, "Save Failed");
                return View(Dependencies.EditViewModelBuilder.Rebuild(model));
            }
        }

        [NavigatablePageActionFilter(DisplayText = "eReturn Details")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns + "," + Role.EReturnAuthoriser)]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns)]
        public ActionResult Create()
        {
            var model = Dependencies.CreateViewModelBuilder.Build();

            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns)]
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid) return View(Dependencies.CreateViewModelBuilder.Rebuild(model));

            var result = Dependencies.CreateCommand.Execute(model);
            var eReturn = ((EReturn)result.Data);

            if (eReturn == null)
            {
                return View(Dependencies.CreateViewModelBuilder.Rebuild(model));
            }

            return RedirectToAction("Edit", new { id = ((EReturn)result.Data).Id });
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.EReturnDelete)]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = Dependencies.DeleteCommand.Execute(id);

            TempData["message"] = result.Success
                ? new SuccessMessage("eReturn was deleted", "Delete successful") as Message
                : new ErrorMessage(string.Join(",", result.Messages), "Delete failed") as Message;

            return RedirectToAction("Back");
        }

        [MultipleButton(MatchFormKey = "action", MatchFormValue = "Submit")]
        [Classes.Security.Attributes.Authorize(Roles = Role.EReturns)]
        [HttpPost]
        public ActionResult Submit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Dependencies.EditCommand.Execute(model);
            var result = Dependencies.SubmitCommand.Execute(model.EReturn.EReturn.Id);

            if (result.Success)
            {
                TempData["message"] = new SuccessMessage(string.Format("eReturn {0} was submitted", model.EReturn.EReturn.EReturnNo), "Submit successful");
                return RedirectToAction("Back");
            }

            model.Message = new ErrorMessage(string.Join(",", result.Messages), "Submit failed") as Message;
            Dependencies.EditViewModelBuilder.Rebuild(model);

            return View("Edit", model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.EReturnAuthoriser)]
        [HttpPost]
        public JsonResult Authorise(ApproveViewModel model)
        {
            var result = Dependencies.AuthoriseCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }
    }
}