namespace BusinessLogic.Extensions
{
    public static class NumberFormatExtension
    {
        public static decimal ToTwoDecimalPlaces(this decimal? value)
        {
            return decimal.Round((value ?? 0), 2);
        }

        public static decimal ToTwoDecimalPlaces(this decimal value)
        {
            return decimal.Round(value, 2);
        }

        public static string SignTrailing(decimal value)
        {
            string amount = value.ToString();
            if (value < 0)
            {
                var nosign = value * -1;
                amount = nosign + "-";
            }
            return amount;
        }
    }
}
