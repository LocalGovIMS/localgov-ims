namespace Api.Controllers.MethodOfPayments
{
    public class MethodOfPaymentModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public decimal MaximumAmount { get; set; }

        public decimal MinimumAmount { get; set; }

        public bool Disabled { get; set; }

        public MethodOfPaymentModel(BusinessLogic.Entities.Mop source)
        {
            Code = source.MopCode;
            Name = source.MopName;
            MaximumAmount = source.MaximumAmount;
            MinimumAmount = source.MinimumAmount;
            Disabled = source.Disabled;
        }
    }
}