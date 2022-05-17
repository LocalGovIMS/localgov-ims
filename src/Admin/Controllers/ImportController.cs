using Admin.Classes.Commands.Import;
using Admin.Models.Import;
using Admin.Models.Shared;
using BusinessLogic.Models;
using BusinessLogic.Security;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
    public class ImportController : BaseController<ImportControllerDependencies>
    {
        public ImportController(ImportControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "Imports")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [NavigatablePageActionFilter(DisplayText = "File Import")]
        [HttpGet]
        public ActionResult FileImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileImport(HttpPostedFileBase postedFile)
        {
            var result = Dependencies.SaveImportFileCommand.Execute(
                new SaveImportFileCommandArgs
                {
                    File = postedFile,
                    Path = Server.MapPath(ConfigurationManager.AppSettings["FileImport.UploadDirectory"])
                });

            if (result.Success)
            {
                return RedirectToAction("Confirm", 
                    new 
                    { 
                        batchReference = ((LoadFromFileResult)result.Data).Import.BatchReference,
                        rowCount = ((LoadFromFileResult)result.Data).RowCount
                    });
            }

            TempData["Message"] = new ErrorMessage(result.Messages);

            return View(); 
        }

        [NavigatablePageActionFilter(DisplayText = "Confirm Import", OnlyComparePath = true)]
        [HttpGet]
        public ActionResult Confirm(string batchReference, int rowCount)
        {
            var model = new ConfirmViewModel() { BatchReference = batchReference, RowCount = rowCount };

            return View(model);
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmViewModel model)
        {
            var result = Dependencies.ProcessImportCommand.Execute(model.BatchReference);

            if (!result.Success)
            {
                TempData["Message"] = new ErrorMessage(result.Messages);

                return RedirectToAction("Confirm", new ConfirmViewModel() { BatchReference = model.BatchReference, RowCount = model.RowCount });
            }

            TempData["Message"] =  new SuccessMessage($"{((ProcessResult)result.Data).NumberOfRowsImported} rows have been imported");

            return RedirectToAction("Search", "Transaction", new { BatchReference = ((ProcessResult)result.Data).Import.BatchReference });
        }
    }
}