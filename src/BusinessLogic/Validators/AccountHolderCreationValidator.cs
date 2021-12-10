using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class AccountHolderStopMessageValidator : IAccountHolderStopMessageValidator
    {
        private readonly IStopMessageService _stopMessaageService;
        public AccountHolderStopMessageValidator(IStopMessageService stopMessageService)
        {
            _stopMessaageService = stopMessageService;
        }

        public IResult Validate(AccountHolder accountHolder)
        {
            if(string.IsNullOrEmpty(accountHolder.StopMessageReference)) 
                return new Result();

            var matchingStopMessage = _stopMessaageService
                .GetAll()
                .FirstOrDefault(x => x.FundCode == accountHolder.FundCode && x.Id == accountHolder.StopMessageReference);

            if (matchingStopMessage is null) 
                return new Result("The fund/stop message combination is not valid");

            return new Result();
        }
    }
}
