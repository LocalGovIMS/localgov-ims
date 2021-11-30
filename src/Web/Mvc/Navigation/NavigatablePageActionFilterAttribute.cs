using System;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc.Navigation
{
    /// <summary>
    /// This attribute can be used to determine which page to return to when clicking a back button.
    /// The attribute is used to identify methods which get added to a navigation stack - meaning we 
    /// can poop items on it as navigate around the application, and then pop them off, so we know 
    /// where to return to when we want to go back.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NavigatablePageActionFilterAttribute : ActionFilterAttribute
    {

        // There are times when we want to clear the navigation stack - such as when we click on a menu
        // item that takes us to a separate area of the application. In that scenario we'd decorate the 
        // action with this attribute, and set this value to true.
        public bool ClearNavigation { get; set; }

        public string DisplayText { get; set; }

        public bool OnlyComparePath { get; set; }

        // In some scenarios, we may want the ability to clear the stack, but not actually add the action to the stack.
        // In those scenarios, set this value to false.
        private bool _addToStack = true;
        public bool AddToStack
        {
            get
            {
                return _addToStack;
            }
            set
            {
                _addToStack = value;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var navigationManager = new NavigationManager();

            if (ClearNavigation) navigationManager.Clear();

            var navigationItem = new NavigationItem
            {
                Url = HttpContext.Current.Request.RawUrl,
                Path = HttpContext.Current.Request.Path,
                DisplayText = this.DisplayText
            };

            if (_addToStack) navigationManager.Push(navigationItem, OnlyComparePath);
        }
    }
}