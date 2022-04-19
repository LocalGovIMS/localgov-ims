using Admin.Models.CheckDigitConfiguration;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.CheckDigitConfiguration
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly ICheckDigitConfigurationService _service;

        public CreateCommand(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.CheckDigitConfiguration()
            {
                ApplySubtraction = model.ApplySubtraction,
                Id = model.Id,
                Modulus = model.Modulus,
                Name = model.Name,
                ResultSubstitutions = model.ResultSubstitutions,
                SourceSubstitutions = model.SourceSubstitutions,
                Type = (int)model.Type,
                Weightings = model.Weightings
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}