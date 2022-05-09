using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators.Payment
{
    public class WeightedSumStrategy : ICheckDigitStrategy
    {
        private int[] _weightings;
        private string _referenceToCheck;
        private int _weightedSum = 0;
        private int _remainder = 0;
        private string _calculatedCheckDigit;

        public WeightedSumStrategy()
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
            ExtractWeightings(args);
            PrepareReferenceToCheck(args);
        }

        private void ExtractWeightings(CheckDigitStrategyArgs args)
        {
            _weightings = args.CheckDigitConfiguration.WeightingsArray();
        }

        private void PrepareReferenceToCheck(CheckDigitStrategyArgs args)
        {
            _referenceToCheck = args.Reference;

            ApplySourceSubstitutions(args);
        }

        private void ApplySourceSubstitutions(CheckDigitStrategyArgs args)
        {
            if (string.IsNullOrEmpty(args.CheckDigitConfiguration.SourceSubstitutions))
                return;

            // TODO: This could be done in an extension method
            _referenceToCheck = args.CheckDigitConfiguration.SourceSubstitutions + args.Reference.Substring(args.CheckDigitConfiguration.SourceSubstitutions.Length);
        }

        private void CalculateCheckDigit(CheckDigitStrategyArgs args)
        {
            GenerateWeightedSum();
            CalculateRemainder(args);
            ApplySubtraction(args);
            ApplyResultSubstitutions(args);
        }

        private void GenerateWeightedSum()
        {
            var charArray = _referenceToCheck.ToCharArray();

            for (var i = 0; i < charArray.Length; i++)
            {
                var characterToCheck = charArray[i];

                if (char.IsDigit(characterToCheck))
                {
                    _weightedSum += int.Parse(characterToCheck.ToString()) * _weightings[i];
                }
            }
        }

        private void CalculateRemainder(CheckDigitStrategyArgs args)
        {
            _remainder = _weightedSum % args.CheckDigitConfiguration.Modulus;
        }

        private void ApplySubtraction(CheckDigitStrategyArgs args)
        {
            if (!args.CheckDigitConfiguration.ApplySubtraction)
                return;

            _remainder = args.CheckDigitConfiguration.Modulus - _remainder;
        }

        private void ApplyResultSubstitutions(CheckDigitStrategyArgs args)
        {
            var conversions = args.CheckDigitConfiguration.ResultSubstitutionsDictionary();

            _calculatedCheckDigit = conversions.ContainsKey(_remainder.ToString())
                    ? conversions[_remainder.ToString()]
                    : _remainder.ToString();
        }

        private void ValidateCheckDigit(CheckDigitStrategyArgs args)
        {
            if (!Equals(args.CheckDigit.ToString(), _calculatedCheckDigit))
                throw new CheckDigitValidationException();
        }
    }
}
