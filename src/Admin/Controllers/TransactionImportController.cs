using Admin.Models.TransactionImport;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
    public class TransactionImportController : BaseController<ITransactionImportControllerDependencies>
    {
        public TransactionImportController(ITransactionImportControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Transaction Imports")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(DisplayText = "Transaction Imports")]
        [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin)]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Search(SearchCriteria criteria)
        {
            var model = Dependencies.ListViewModelBuilder.Build(criteria);

            return View("List", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return PartialView(model);
        }
    }
}