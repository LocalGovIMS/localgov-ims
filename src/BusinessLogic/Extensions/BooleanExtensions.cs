namespace BusinessLogic.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string ToTrueFalseString(this bool value)
        {
            return value ? "True" : "False";
        }
    }
}
