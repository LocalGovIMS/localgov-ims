using Admin.Models.Transfer;
using BusinessLogic.Models;
using BusinessLogic.Security;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public class TransferController : BaseController<ITransferControllerDependencies>
    {
        public TransferController(ITransferControllerDependencies dependencies) : base(dependencies)
        {
        }

        [NavigatablePageActionFilter(ClearNavigation = true, DisplayText = "Transfer")]
        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer)]
        [HttpGet]
        public ActionResult Index()
        {
            var model = Dependencies.TransferViewModelBuilder.Build();
            
            return View(model);
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer)]
        [HttpPost]
        public JsonResult SubmitTransfers(TransferViewModel model)
        {
            var result = Dependencies.TransferCommand.Execute(model);
            
            return Json(new { ok = result.Success, message = result.Success ? result.Data : string.Join(", ", result.Messages) });
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer)]
        [HttpPost]
        public JsonResult ValidateTransferItem(TransferItem model)
        {
            var result = Dependencies.ValidateTransferItemCommand.Execute(model);
            
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }
    }
}