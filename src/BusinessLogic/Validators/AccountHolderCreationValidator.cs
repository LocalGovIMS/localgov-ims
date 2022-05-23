using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators
{
    public class AccountHolderFundMessageValidator : IAccountHolderFundMessageValidator
    {
        private readonly IFundMessageService _fundMessageService;
        public AccountHolderFundMessageValidator(IFundMessageService fundMessageService)
        {
            _fundMessageService = fundMessageService;
        }

        public IResult Validate(AccountHolder accountHolder)
        {
            if(!accountHolder.FundMessageId.HasValue) 
                return new Result();

            var matchingFundMessage = _fundMessageService.GetById(accountHolder.FundMessageId.Value);

            if (matchingFundMessage is null) 
                return new Result("The message is not valid");

            if (matchingFundMessage.FundCode != accountHolder.FundCode) 
                return new Result("The message is not valid");

            return new Result();
        }
    }
}
