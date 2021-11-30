using Admin.Models.AccountHolder;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public class AccountHolderController : BaseController<IAccountHolderControllerDependencies>
    {
        public AccountHolderController(IAccountHolderControllerDependencies dependencies)
            : base(dependencies)
        {
        }

        [HttpGet]
        public ActionResult List()
        {
            return RedirectToAction("Search");
        }

        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchViewModel criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Account Holder Details")]
        [Classes.Security.Attributes.Authorize(Roles = Role.TransactionDetails)]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);
            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer + "," + Role.TransactionJournal)]
        [HttpPost]
        public JsonResult Lookup(LookupViewModel model)
        {
            var result = Dependencies.LookupAccountHolderCommand.Execute(model);
            return Json(new
            {
                ok = result.Success,
                message = string.Join(", ", result.Messages),
                data = result.Data
            });
        }
    }
}