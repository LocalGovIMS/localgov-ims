using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Data.Entity;
using System.Linq;


namespace DataAccess.Repositories
{
    public class AccountValidationRepository : Repository<AccountValidation>, IAccountValidationRepository
    {
        public AccountValidationRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public AccountValidation GetAccountValidation(string validationReference)
        {
            var validation = IncomeDbContext.AccountValidations
                .Include(x => x.AccountValidationWeightings)
                .ApplyFilters(Filters)
                .First(x => x.ValidationReference == validationReference);

            return new AccountValidation()
            {
                AccountValidationWeightings = validation.AccountValidationWeightings
                    .Select(x => new AccountValidationWeighting
                    {
                        AccountValidation = null,
                        Id = x.Id,
                        ValidationReference = x.ValidationReference,
                        Digit1Weighting = x.Digit1Weighting,
                        Digit2Weighting = x.Digit2Weighting,
                        Digit3Weighting = x.Digit3Weighting,
                        Digit4Weighting = x.Digit4Weighting,
                        Digit5Weighting = x.Digit5Weighting,
                        Digit6Weighting = x.Digit6Weighting,
                        Digit7Weighting = x.Digit7Weighting,
                        Digit8Weighting = x.Digit8Weighting,
                        Digit9Weighting = x.Digit9Weighting,
                        Digit10Weighting = x.Digit10Weighting,
                        Digit11Weighting = x.Digit11Weighting,
                        Digit12Weighting = x.Digit12Weighting,
                        Digit13Weighting = x.Digit13Weighting,
                        Digit14Weighting = x.Digit14Weighting,
                        Digit15Weighting = x.Digit15Weighting,
                        Digit16Weighting = x.Digit16Weighting,
                        Digit17Weighting = x.Digit17Weighting,
                        Digit18Weighting = x.Digit18Weighting,
                        Digit19Weighting = x.Digit19Weighting,
                        Digit20Weighting = x.Digit20Weighting,
                        Digit21Weighting = x.Digit21Weighting,
                        Digit22Weighting = x.Digit22Weighting,
                        Digit23Weighting = x.Digit23Weighting,
                        Digit24Weighting = x.Digit24Weighting,
                        Digit25Weighting = x.Digit25Weighting,
                        Digit26Weighting = x.Digit26Weighting,
                        Digit27Weighting = x.Digit27Weighting,
                        Digit28Weighting = x.Digit28Weighting,
                        Digit29Weighting = x.Digit29Weighting,
                        Digit30Weighting = x.Digit30Weighting
                    }
                    ).ToList(),
                CheckDigitCalcAlphaReplace = validation.CheckDigitCalcAlphaReplace,
                InputMask = validation.InputMask,
                MaxLength = validation.MaxLength,
                MinLength = validation.MinLength,
                Modulus = validation.Modulus,
                SubtractFlag = validation.SubtractFlag,
                TenConversion = validation.TenConversion,
                Name = validation.Name,
                ValidationReference = validation.ValidationReference,
                CanNotBeNumeric = validation.CanNotBeNumeric
            };
        }
    }
}