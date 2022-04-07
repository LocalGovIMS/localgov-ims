using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators.Payment
{
    public class DynixLibraryStrategy : ICheckDigitStrategy
    {
        private int[] _weightings;
        private int _sum = 0;
        private string _calculatedCheckDigit;

        public DynixLibraryStrategy()
        {
        }

        public void Validate(CheckDigitStrategyArgs args)
        {
            ValidateArgs(args);

            Initialise(args);

            CalculateCheckDigit(args);

            ValidateCheckDigit(args);
        }

        private void ValidateArgs(CheckDigitStrategyArgs args)
        {
            if (string.IsNullOrEmpty(args.CheckDigitConfiguration.Weightings))
                throw new CheckDigitValidationException("No check digit weightings specified");
        }

        private void Initialise(CheckDigitStrategyArgs args)
        {
            _weightings = args.CheckDigitConfiguration.WeightingsArray();
        }

        private void CalculateCheckDigit(CheckDigitStrategyArgs args)
        {
            GenerateSum(args);
            SetCheckDigit(args);
        }

        private void GenerateSum(CheckDigitStrategyArgs args)
        {
            var referenceToCheck = args.Reference.WithoutCheckDigit().ToCharArray();

            for (var i = 0; i < referenceToCheck.Length-1; i++)
            {
                if (!char.IsDigit(referenceToCheck[i]))
                    continue;
                                
                var digitToCheck = int.Parse(referenceToCheck[i].ToString());

                if (i.IsOdd())
                {
                    _sum += digitToCheck;
                }
                else
                {
                    var value = digitToCheck * _weightings[i];

                    if (value > 10)
                    {
                        value -= 10;
                    }

                    if (digitToCheck > 4)
                    {
                        value++;
                    }

                    _sum += value;
                }
            }
        }

        private void SetCheckDigit(CheckDigitStrategyArgs args)
        {
            _calculatedCheckDigit = (args.CheckDigitConfiguration.Modulus - (_sum % args.CheckDigitConfiguration.Modulus)).ToString();
        }

        private void ValidateCheckDigit(CheckDigitStrategyArgs args)
        {
            if (!Equals(args.CheckDigit.ToString(), _calculatedCheckDigit))
                throw new CheckDigitValidationException();
        }
    }
}
