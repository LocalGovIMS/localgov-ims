using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;

namespace BusinessLogic.Validators.Payment
{
    public class InputMaskValidator : AbstractValidator
    {
        private readonly ILog _logger;
        private readonly Func<CheckDigitType, ICheckDigitStrategy> _checkDigitStrategyFactory;

        public InputMaskValidator(
            ILog logger,
            Func<CheckDigitType, ICheckDigitStrategy> checkDigitStrategyFactory)
        {
            _logger = logger;
            _checkDigitStrategyFactory = checkDigitStrategyFactory;
        }

        protected override void OnValidate(PaymentValidationArgs args)
        {
            ValidateReferenceMatchesMask(args);
        }

        public void ValidateReferenceMatchesMask(PaymentValidationArgs args)
        {

            var maskChars = args.AccountReferenceValidator.InputMask.ToCharArray();
            var capitalisedReference = args.Reference.ToUpper();

            for (var index = 0; index < capitalisedReference.Length; index++)
            {
                char referenceChar = capitalisedReference[index];
                switch (maskChars[index])
                {
                    case '$':
                        if (!char.IsLetter(referenceChar))
                            throw new PaymentValidationException();
                        break;

                    case '#':
                        if (!char.IsDigit(referenceChar))
                            throw new PaymentValidationException();
                        break;

                    case '?':
                        if (!char.IsLetterOrDigit(referenceChar))
                            throw new PaymentValidationException();
                        break;

                    case '*':
                        if (!char.IsLetterOrDigit(referenceChar)
                            && !referenceChar.Equals(' ')
                            && !referenceChar.Equals('/')
                            && !referenceChar.Equals('-'))
                            throw new PaymentValidationException();
                        break;

                    case '@':
                        if (!CheckDigitIsValid(capitalisedReference, referenceChar, args.AccountReferenceValidator))
                            throw new PaymentValidationException();
                        break;

                    default:
                        if (referenceChar != maskChars[index])
                            throw new PaymentValidationException();
                        break;
                }
            }          
        }

        private bool CheckDigitIsValid(string reference, char checkDigit, Entities.AccountReferenceValidator validator)
        {
            try
            {
                var checkDigitStrategy = _checkDigitStrategyFactory((CheckDigitType)validator.CheckDigitConfiguration.Type);

                checkDigitStrategy.Validate(new CheckDigitStrategyArgs()
                {
                    Reference = reference,
                    CheckDigit = checkDigit,
                    CheckDigitConfiguration = validator.CheckDigitConfiguration
                });

                return true;
            }
            catch(Exception ex)
            {
                _logger.Error(null, ex);

                return false;
            }
        }
    }
}
