using System.Text.RegularExpressions;

namespace BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string value)
        {
            // start by converting entire string to lower case
            var lowerCase = value.ToLower();
            // matches the first sentence of a string, as well as subsequent sentences
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            // MatchEvaluator delegate defines replacement of setence starts to uppercase
            return r.Replace(lowerCase, s => s.Value.ToUpper());
        }
    }
}
