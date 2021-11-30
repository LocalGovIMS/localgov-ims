using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System;
using System.Linq;

namespace BusinessLogic.Validators
{
    // HIGH: This wants to use ValidationResult
    public class AccountReferenceValidator : IAccountReferenceValidator
    {
        private readonly ILog _logger;
        private readonly IFundService _fundService;
        private readonly IAccountHolderService _accountHolderService;
        private readonly ISecurityContext _securityContext;

        private const string DefaultAccountReferenceInvalidMessage = "The account reference is not valid";

        public AccountReferenceValidator(ILog logger
            , IFundService fundService
            , IAccountHolderService accountHolderService
            , ISecurityContext securityContext)
        {
            _logger = logger;
            _fundService = fundService;
            _accountHolderService = accountHolderService;
            _securityContext = securityContext;
        }

        public IResult ValidateReference(string reference, string fundCode, decimal amount, AccountReferenceValidationSource source)
        {
            _logger.DebugFormat("Validating reference for: {0}, fundCode: {1}, amount: {2}, source: {3}", reference, fundCode, amount, source);

            try
            {
                var fund = _fundService.GetByFundCode(fundCode);
                if (fund == null) throw new NullReferenceException(string.Format("Fund is null: {0}", fundCode));

                _logger.DebugFormat("Found Fund?: {0}", fund != null); // Will log true/false is fund is found/not found

                var validationRule = _accountHolderService.GetAccountValidation(fund.ValidationReference);
                if (validationRule == null) throw new NullReferenceException(string.Format("Validation rule is null for fund {0}", fundCode));

                _logger.DebugFormat("Found Validation Rule?: {0}", validationRule != null); // Will log true/false is fund is found/not found

                if (amount > fund.MaximumAmount)
                {
                    return new Result("The maximum amount you can pay for this type is £" + decimal.Round((decimal)fund.MaximumAmount, 2));
                }

                var referenceLengthValidation = ValidateReferenceLength(reference, validationRule);
                if (!referenceLengthValidation.Success) return referenceLengthValidation;

                if (validationRule.CanNotBeNumeric && ValidateNumeric(reference))
                {
                    return new Result("A numeric reference is invalid for this payment type");
                }

                Result validationResponse = null;

                if (fund.AccountExist)
                {
                    validationResponse = ValidateAccountExistsIfRequired(reference, fund);
                    if (!validationResponse.Success) return validationResponse;
                }
                else
                {
                    validationResponse = ValidateReferenceMatchesMask(reference, validationRule.InputMask, validationRule);
                    if (!validationResponse.Success) return validationResponse;
                }

                if (source == AccountReferenceValidationSource.Payments)
                {
                    return new Result();
                }

                return new Result("Unrecognised source");
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result("Unable to validate the account reference");
            }
        }

        private Result ValidateAccountExistsIfRequired(string reference, Fund fund)
        {
            try
            {
                var account = _accountHolderService.GetByAccountReference(reference, fund.FundCode);
                if (fund.AccountExist && account == null)
                {
                    return new Result(DefaultAccountReferenceInvalidMessage);
                }
                if (account != null && !string.IsNullOrWhiteSpace(account.StopMessageReference) && !_securityContext.IsInRole(Security.Role.Finance)) // finance can post/pay to stopped accounts
                {
                    return new Result("This account has been stopped");
                }

                return new Result();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result(DefaultAccountReferenceInvalidMessage);
            }
        }

        // TODO: As far as I can see, this in only public so that it can be unit tested
        public Result ValidateReferenceMatchesMask(string reference, string inputMask, AccountValidation validationRule)
        {
            try
            {
                var maskChars = inputMask.ToCharArray();
                reference = reference.ToUpper();

                for (var index = 0; index < reference.Length; index++)
                {
                    char referenceChar = reference[index];
                    switch (maskChars[index])
                    {
                        case '$':
                            if (!char.IsLetter(referenceChar))
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;

                        case '#':
                            if (!char.IsDigit(referenceChar))
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;

                        case '?':
                            if (!char.IsLetterOrDigit(referenceChar))
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;

                        case '*':
                            if (!char.IsLetterOrDigit(referenceChar)
                                && !referenceChar.Equals(' ')
                                && !referenceChar.Equals('/')
                                && !referenceChar.Equals('-'))
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;

                        case '@':
                            if (!CheckDigitIsValid(reference, referenceChar, validationRule))
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;

                        default:
                            if (referenceChar != maskChars[index])
                                return new Result(DefaultAccountReferenceInvalidMessage);
                            break;
                    }
                }
                return new Result();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
                return new Result(DefaultAccountReferenceInvalidMessage);
            }
        }

        private static bool CheckDigitIsValid(string reference, char referenceChar, AccountValidation validationRule)
        {
            if (validationRule.AccountValidationWeightings == null) return false;
            var checkReference = reference;
            if (!string.IsNullOrEmpty(validationRule.CheckDigitCalcAlphaReplace))
            {
                checkReference = validationRule.CheckDigitCalcAlphaReplace + reference.Substring(validationRule.CheckDigitCalcAlphaReplace.Length);
            }

            var weightings = validationRule.AccountValidationWeightings.FirstOrDefault();

            var referenceWeightings = new int[30];
            referenceWeightings[0] = int.Parse(weightings.Digit1Weighting);
            referenceWeightings[1] = int.Parse(weightings.Digit2Weighting);
            referenceWeightings[2] = int.Parse(weightings.Digit3Weighting);
            referenceWeightings[3] = int.Parse(weightings.Digit4Weighting);
            referenceWeightings[4] = int.Parse(weightings.Digit5Weighting);
            referenceWeightings[5] = int.Parse(weightings.Digit6Weighting);
            referenceWeightings[6] = int.Parse(weightings.Digit7Weighting);
            referenceWeightings[7] = int.Parse(weightings.Digit8Weighting);
            referenceWeightings[8] = int.Parse(weightings.Digit9Weighting);
            referenceWeightings[9] = int.Parse(weightings.Digit10Weighting);
            referenceWeightings[10] = int.Parse(weightings.Digit11Weighting);
            referenceWeightings[11] = int.Parse(weightings.Digit12Weighting);
            referenceWeightings[12] = int.Parse(weightings.Digit13Weighting);
            referenceWeightings[13] = int.Parse(weightings.Digit14Weighting);
            referenceWeightings[14] = int.Parse(weightings.Digit15Weighting);
            referenceWeightings[15] = int.Parse(weightings.Digit16Weighting);
            referenceWeightings[16] = int.Parse(weightings.Digit17Weighting);
            referenceWeightings[17] = int.Parse(weightings.Digit18Weighting);
            referenceWeightings[18] = int.Parse(weightings.Digit19Weighting);
            referenceWeightings[19] = int.Parse(weightings.Digit20Weighting);
            referenceWeightings[20] = int.Parse(weightings.Digit21Weighting);
            referenceWeightings[21] = int.Parse(weightings.Digit22Weighting);
            referenceWeightings[22] = int.Parse(weightings.Digit23Weighting);
            referenceWeightings[23] = int.Parse(weightings.Digit24Weighting);
            referenceWeightings[24] = int.Parse(weightings.Digit25Weighting);
            referenceWeightings[25] = int.Parse(weightings.Digit26Weighting);
            referenceWeightings[26] = int.Parse(weightings.Digit27Weighting);
            referenceWeightings[27] = int.Parse(weightings.Digit28Weighting);
            referenceWeightings[28] = int.Parse(weightings.Digit29Weighting);
            referenceWeightings[29] = int.Parse(weightings.Digit30Weighting);

            var total = 0;
            var charArray = checkReference.ToCharArray();
            for (var index = 0; index < charArray.Length; index++)
            {
                var checkChar = charArray[index];
                if (char.IsDigit(checkChar))
                {
                    if (validationRule.Modulus == "OE")
                    {
                        if (index % 2 != 0)
                        {
                            if (index != (Convert.ToInt32(validationRule.MaxLength) - 1))
                            {
                                total += int.Parse(checkChar.ToString());
                            }
                        }
                        else
                        {
                            var value = int.Parse(checkChar.ToString()) * referenceWeightings[index];

                            if (value > 10)
                            {
                                value -= 10;
                            }

                            if (int.Parse(checkChar.ToString()) > 4)
                            {
                                value++;
                            }

                            total += value;
                        }
                    }
                    else
                    {
                        total += int.Parse(checkChar.ToString()) * referenceWeightings[index];
                    }
                }
            }

            string expectedChar;
            if (validationRule.Modulus == "OE")
            {
                expectedChar = (10 - (total % 10)).ToString();
            }
            else
            {
                var result = total % int.Parse(validationRule.Modulus);
                expectedChar = (result == 10) ? validationRule.TenConversion : result.ToString();
            }

            return Equals(referenceChar.ToString(), expectedChar);
        }

        private static Result ValidateReferenceLength(string reference, AccountValidation validationRule)
        {
            if (reference.Length > Convert.ToInt32(validationRule.MaxLength) ||
                reference.Length < Convert.ToInt32(validationRule.MinLength))
            {
                return new Result(DefaultAccountReferenceInvalidMessage);
            }

            return new Result();
        }

        private static bool ValidateNumeric(string reference)
        {
            bool canConvert = long.TryParse(reference, out _);

            if (canConvert == true)
            {
                return true;
            }

            return false;
        }
    }
}