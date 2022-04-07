﻿using System.Text.RegularExpressions;

namespace BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        private const string AlphaRegex = @"^[a-zA-Z]+$";
        private const string NumericRegex = @"^[0-9]+$";
        private const string AlphaNumericRegex = @"^[a-zA-Z0-9]+$";

        public static string ToSentenceCase(this string value)
        {
            // start by converting entire string to lower case
            var lowerCase = value.ToLower();
            // matches the first sentence of a string, as well as subsequent sentences
            var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);
            // MatchEvaluator delegate defines replacement of setence starts to uppercase
            return r.Replace(lowerCase, s => s.Value.ToUpper());
        }

        public static bool IsAlpha(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, AlphaRegex);
        }

        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, NumericRegex);
        }

        public static bool IsAlphaNumeric(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, AlphaNumericRegex);
        }

        public static string WithoutCheckDigit(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return value.Substring(0, value.Length - 1);
        }
    }
}
