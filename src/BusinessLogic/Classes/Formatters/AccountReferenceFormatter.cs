using BusinessLogic.Interfaces.Formatters;

namespace BusinessLogic.Classes.Formatters
{
    public class AccountReferenceFormatter : IAccountReferenceFormatter
    {
        public string Format(string type, string reference)
        {
            switch (type.ToLower())
            {
                case "parkingfine":
                    if (reference.Length == 8)
                        reference = "BJ" + reference;
                    else
                        reference = "BJ" + reference + "A";
                    break;

                case "fixedpenaltynotice":
                    if (reference.Length == 8)
                        reference = "FP" + reference;
                    else
                        reference = "FP" + reference + "A";
                    break;
            };

            return reference;
        }
    }
}
