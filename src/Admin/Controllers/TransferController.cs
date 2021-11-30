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

        // TODO: Investigate why the error handling commented out
        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer)]
        [HttpPost]
        public JsonResult SubmitTransfers(TransferViewModel model)
        {
            //try
            //{
            var result = Dependencies.TransferCommand.Execute(model);
            return Json(new { ok = result.Success, message = result.Success ? result.Data : string.Join(", ", result.Messages) });
            //}
            //catch (Exception e)
            //{
            //    Dependencies.Log.Error(string.Format("Error processing transfers for model: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(model)), e);
            //    return Json(new { ok = false, message = "Unable to process this request" });
            //}
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer)]
        [HttpPost]
        public JsonResult ValidateTransferItem(TransferItem model)
        {
            //try
            //{
            var result = Dependencies.ValidateTransferItemCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
            //}
            //catch (Exception e)
            //{
            //    Dependencies.Log.Error(string.Format("Error processing transfers for model: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(model)), e);
            //    return Json(new { ok = false, message = "Unable to process this request" });
            //}
        }
    }
}