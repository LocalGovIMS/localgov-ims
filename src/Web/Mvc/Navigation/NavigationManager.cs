using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Mvc.Navigation
{
    /// <summary>
    /// This represents our navigation stack - used for allowing us to navigate backwards 
    /// through our web app.
    /// </summary>
    public class NavigationManager
    {
        private const string sessionKey = "NAVIGATION__STACK";
        public Stack<NavigationItem> NavigationItems { get; private set; }

        // We store the stack in session - if it exists, we'll retrieve it, if it doesn't then
        // we create an instance and add it to session
        public NavigationManager()
        {
            if (HttpContext.Current.Session[sessionKey] != null)
            {
                NavigationItems = HttpContext.Current.Session[sessionKey] as Stack<NavigationItem>;
            }
            else
            {
                NavigationItems = new Stack<NavigationItem>();
                Save();
            }
        }

        public void Push(NavigationItem navigationItem, bool onlyComparePath)
        {
            // IF the item is already in the list - remove all items up to that point
            if (NavigationItems.Any() && NavigationItems.Contains(navigationItem))
            {
                var matches = false;
                while (matches == false)
                {
                    if (NavigationItems.Peek().Equals(navigationItem))
                    {
                        matches = true;
                        break;
                    }

                    NavigationItems.Pop();
                }
            }

            // If the item on the top matches the item being passed in, then pop it off, 
            // and replace it with the new one - as we're not interested in navigating 
            // back to the same page that we're already on! In addition, if we add a page
            // and it then fails model validation, when we resubmit, it would get added 
            // again, if we didn't perform this logic.
            if (onlyComparePath)
            {
                if (NavigationItems.Any() && NavigationItems.Peek().Path == navigationItem.Path)
                {
                    NavigationItems.Pop();
                }
            }
            else
            {
                if (NavigationItems.Any() && NavigationItems.Peek().Equals(navigationItem))
                {
                    NavigationItems.Pop();
                }
            }

            NavigationItems.Push(navigationItem);

            Save();
        }

        public NavigationItem Pop()
        {
            if (!NavigationItems.Any()) return null;

            if ((HttpContext.Current.Request.UrlReferrer != null)
                && (HttpContext.Current.Request.UrlReferrer.PathAndQuery == NavigationItems.Peek().Url)
                && (NavigationItems.Count() > 1))
            {
                NavigationItems.Pop();
            }

            if (!NavigationItems.Any()) return null;

            var navigationItem = NavigationItems.Pop();
            Save();

            return navigationItem;
        }

        public void Clear()
        {
            NavigationItems.Clear();
            Save();
        }

        private void Save()
        {
            HttpContext.Current.Session[sessionKey] = NavigationItems;
        }
    }
}