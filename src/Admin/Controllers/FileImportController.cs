using Admin.Classes.Commands.FileImport;
using Admin.Models.Import;
using Admin.Models.Shared;
using BusinessLogic.Models.Import;
using BusinessLogic.Security;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    [Classes.Security.Attributes.Authorize(Roles = Role.Finance)]
    public class FileImportController : BaseController<FileImportControllerDependencies>
    {
        public FileImportController(FileImportControllerDependencies dependecies)
            : base(dependecies)
        {
        }

        [NavigatablePageActionFilter(DisplayText = "File Imports")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [NavigatablePageActionFilter(DisplayText = "File Import")]
        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase postedFile)
        {
            var result = Dependencies.SaveCommand.Execute(
                new SaveCommandArgs
                {
                    File = postedFile,
                    Path = Server.MapPath(ConfigurationManager.AppSettings["FileImport.UploadDirectory"])
                });

            if (result.Success)
            {
                return RedirectToAction("Confirm", 
                    new 
                    { 
                        importId = ((LoadFromFileResult)result.Data).FileImport.ImportId,
                        rowCount = ((LoadFromFileResult)result.Data).RowCount
                    });
            }

            TempData["Message"] = new ErrorMessage(result.Messages);

            return View(); 
        }

        [NavigatablePageActionFilter(DisplayText = "Confirm Import", OnlyComparePath = true)]
        [HttpGet]
        public ActionResult Confirm(int importId, int rowCount)
        {
            var model = new ConfirmViewModel() { ImportId = importId, RowCount = rowCount };

            return View(model);
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmViewModel model)
        {
            var result = Dependencies.ProcessCommand.Execute(model.ImportId);

            if (!result.Success)
            {
                TempData["Message"] = new ErrorMessage(result.Messages);

                return RedirectToAction("Confirm", new ConfirmViewModel() { ImportId = model.ImportId, RowCount = model.RowCount });
            }

            TempData["Message"] =  new SuccessMessage($"{((ProcessResult)result.Data).NumberOfRowsImported} rows have been imported");

            return RedirectToAction("Search", "Transaction", new { ImportId = ((ProcessResult)result.Data).FileImport.ImportId });
        }
    }
}