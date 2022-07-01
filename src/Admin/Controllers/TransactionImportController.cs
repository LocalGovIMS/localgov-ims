using Admin.Models.TransactionImport;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.SystemAdmin + "," + Role.Finance)]
    public class TransactionImportController : BaseController<ITransactionImportControllerDependencies>
    {
        public TransactionImportController(ITransactionImportControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Transaction Imports")]
        [HttpGet]
        public ActionResult List()
        {
            var model = Dependencies.ListViewModelBuilder.Build();

            return View("List", model);
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Transaction Imports")]
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

        [NavigatablePageActionFilter(DisplayText = "Transaction Import Details")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = Dependencies.DetailsViewModelBuilder.Build(id);

            return View(model);
        }
    }
}