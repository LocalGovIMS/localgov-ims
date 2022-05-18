using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators
{
    public class AccountHolderStopMessageValidator : IAccountHolderStopMessageValidator
    {
        private readonly IStopMessageService _stopMessageService;
        public AccountHolderStopMessageValidator(IStopMessageService stopMessageService)
        {
            _stopMessageService = stopMessageService;
        }

        public IResult Validate(AccountHolder accountHolder)
        {
            if(!accountHolder.StopMessageId.HasValue) 
                return new Result();

            var matchingStopMessage = _stopMessageService.GetById(accountHolder.StopMessageId.Value);

            if (matchingStopMessage is null) 
                return new Result("The stop message is not valid");

            if (matchingStopMessage.FundCode != accountHolder.FundCode) 
                return new Result("The stop message is not valid");

            return new Result();
        }
    }
}
