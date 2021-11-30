using System;

namespace Web.Mvc.Navigation
{
    /// <summary>
    /// This class stores all the information needed to return to a page that we 
    /// were previously visiting. Using this information, when the user clicks a
    /// back button, we can navigate them back to the previous page, with any
    /// querystring/route parameters specified.
    /// </summary>
    [Serializable]
    public class NavigationItem : IEquatable<NavigationItem>
    {
        public string Url { get; set; }
        public string Path { get; set; }
        public string DisplayText { get; set; }

        public bool Equals(NavigationItem other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                // We only care about the controller/action combination. if a user alters, for example, 
                // search criteria on a search page, we'll just override any that was previously used.
                return string.Equals(Url, other.Url);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as NavigationItem);
        }

        /// <see cref="http://www.aaronstannard.com/overriding-equality-in-dotnet/"/>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = (int)2166136261; //a prime number

                int prime2 = 16777619;

                // We only care about the controller/action combination. if a user alters, for example, 
                // search criteria on a search page, we'll just override any that was previously used.
                hashCode = hashCode * prime2 ^ (string.IsNullOrWhiteSpace(Url) ? 0 : Url.GetHashCode());

                return hashCode;
            }
        }
    }
}