using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class AccountHolderFundMessageValidator : IAccountHolderFundMessageValidator
    {
        private readonly IFundMessageService _fundMessageService;
        private List<Entities.FundMessage> _fundMessages = new List<Entities.FundMessage>();

        public AccountHolderFundMessageValidator(IFundMessageService fundMessageService)
        {
            _fundMessageService = fundMessageService;
        }

        public IResult Validate(AccountHolder accountHolder)
        {
            if (!accountHolder.FundMessageId.HasValue)
                return new Result();

            LoadFundMessages();

            var matchingFundMessage = _fundMessages.FirstOrDefault(x => x.Id == accountHolder.FundMessageId.Value);

            if (matchingFundMessage is null)
                return new Result("The fund message is not valid");

            if (matchingFundMessage.FundCode != accountHolder.FundCode)
                return new Result("The fund message is not valid for the fund code specified");

            return new Result();
        }

        private void LoadFundMessages()
        {
            if (_fundMessages.Any()) return;

            _fundMessages = _fundMessageService.GetAll().ToList();
        }
    }
}
