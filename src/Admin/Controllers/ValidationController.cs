using BusinessLogic.Models;
using BusinessLogic.Security;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ValidationController : BaseController<IValidationControllerDependencies>
    {
        public ValidationController(IValidationControllerDependencies dependencies) : base(dependencies)
        {
        }

        [Classes.Security.Attributes.Authorize(Roles = Role.Transfer + "," + Role.TransactionJournal)]
        [HttpPost]
        public JsonResult ValidateTransferItem(TransferItem model)
        {
            var result = Dependencies.ValidateTransferItemCommand.Execute(model);
            return Json(new { ok = result.Success, message = string.Join(", ", result.Messages) });
        }
    }
}