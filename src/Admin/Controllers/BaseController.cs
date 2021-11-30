using Admin.Interfaces.Commands;
using Admin.Models.Shared;
using System;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.Controllers
{
    public abstract class BaseController<T> : Controller where T : IBaseControllerDependencies
    {
        protected readonly T Dependencies;

        protected BaseController(T dependencies)
        {
            if (dependencies == null) throw new ArgumentNullException("dependencies");
            Dependencies = dependencies;
        }

        [HttpGet]
        public ActionResult Back()
        {
            return GoBack();
        }

        [HttpGet]
        public ActionResult Exit()
        {
            return GoBack();
        }

        private ActionResult GoBack()
        {
            var navigationManager = new NavigationManager(); // TODO: Inject this. Will need to extract and interface

            var navigationItem = navigationManager.Pop();

            Dependencies.Log.Info(string.Format("Retrieved navigation item: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(navigationItem)));

            // Hopefully we can return back to our nav item...
            if (navigationItem != null)
                return Redirect(navigationItem.Url);

            var controllerName = "Home";
            var actionName = "Index";

            var routeValues = HttpContext.Request.RequestContext.RouteData.Values;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey("controller"))
                {
                    controllerName = routeValues["controller"].ToString();
                }
            }

            Dependencies.Log.Info(string.Format("Navigation item is null. Returning to '{0}' action on controller '{0}'", controllerName));

            return RedirectToAction(actionName, controllerName);
        }

        // TODO: Remove the Generic Processers methods - they just complicate things and aren't very useful as
        // when errors occur they don't handle and model rebuilding - so for models which use reference data they're useless

        #region Generic Processers
        // This code is to reduce repeating code in the controllers. It's just a few generic methods with overloads, that's all. Nowt scary or complicated.

        protected ActionResult BaseEdit<ViewModel>(ViewModel model, IModelCommand<ViewModel> command)
        {
            return BaseEdit(model, command, "Back");
        }

        protected ActionResult BaseEdit<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string successRedirectToAction)
        {
            return BaseCommandHandler(model, command, "Edit", successRedirectToAction);
        }

        protected ActionResult BaseCreate<ViewModel>(ViewModel model, IModelCommand<ViewModel> command)
        {
            return BaseCreate(model, command, "Back");
        }

        protected ActionResult BaseCreate<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string successRedirectToAction)
        {
            return BaseCommandHandler(model, command, "Create", successRedirectToAction);
        }

        protected ActionResult BaseDelete<ViewModel>(ViewModel model, IModelCommand<ViewModel> command)
        {
            return BaseDelete(model, command, "Back");
        }

        protected ActionResult BaseDelete<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string successRedirectToAction)
        {
            return BaseDelete(model, command, successRedirectToAction, null);
        }

        protected ActionResult BaseDelete<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string successRedirectToAction, object routeValues)
        {
            return BaseCommandHandler(model, command, "Delete", successRedirectToAction, routeValues);
        }

        protected ActionResult BaseCommandHandler<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string action)
        {
            return BaseCommandHandler(model, command, action, "Back");
        }

        protected ActionResult BaseCommandHandler<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string action, string successRedirectToAction)
        {
            return BaseCommandHandler(model, command, action, successRedirectToAction, null);
        }

        protected ActionResult BaseCommandHandler<ViewModel>(ViewModel model, IModelCommand<ViewModel> command, string action, string successRedirectToAction, object routeValues)
        {
            if (!ModelState.IsValid) return View(action, model);

            var result = command.Execute(model);

            if (result.Success)
                if (routeValues == null)
                {
                    return RedirectToAction(successRedirectToAction);
                }
                else
                {
                    return RedirectToAction(successRedirectToAction, routeValues);
                }
            else
            {
                var errorMsg = new ErrorMessage(result.Messages);

                TempData["Message"] = errorMsg;
                return View(action, model);
            }
        }

        #endregion  
    }
}