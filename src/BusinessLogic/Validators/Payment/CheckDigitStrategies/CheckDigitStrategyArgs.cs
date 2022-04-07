using BusinessLogic.Entities;

namespace BusinessLogic.Validators.Payment
{
    public class CheckDigitStrategyArgs
    {
        public string Reference { get; set; }
        public char CheckDigit { get; set; }
        public CheckDigitConfiguration CheckDigitConfiguration { get; set; }
    }
}