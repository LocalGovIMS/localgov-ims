using Admin.Models.AccountReferenceValidator;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.AccountReferenceValidator
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IAccountReferenceValidatorService _service;

        public CreateCommand(ILog log
            , IAccountReferenceValidatorService accountReferenceValidatorService)
            : base(log)
        {
            _service = accountReferenceValidatorService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.AccountReferenceValidator()
            {
                CharacterType = (CharacterType?)model.CharacterType,
                CheckDigitConfigurationId = model.CheckDigitConfigurationId,
                Id = model.Id,
                InputMask = model.InputMask,
                MaxLength = model.MaxLength,
                MinLength = model.MinLength,
                Name = model.Name,
                Regex = model.Regex
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}