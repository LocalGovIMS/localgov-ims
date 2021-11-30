using System.Web.Mvc;

namespace PaymentPortal.Classes
{
    public class PaymentTypeListItem : SelectListItem
    {
        public object ReferenceFieldLabel { get; set; }
        public object ReferenceFieldMessage { get; set; }
    }
}