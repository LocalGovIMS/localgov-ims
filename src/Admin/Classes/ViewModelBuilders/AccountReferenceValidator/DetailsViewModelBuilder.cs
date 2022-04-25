using Admin.Models.AccountReferenceValidator;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.AccountReferenceValidator
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IAccountReferenceValidatorService _service;

        public DetailsViewModelBuilder(ILog log
            , IAccountReferenceValidatorService checkDigitConfigurationService)
            : base(log)
        {
            _service = checkDigitConfigurationService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _service.Get(id);

            return new DetailsViewModel()
            {
                CharacterType = data.CharacterType.Value, // TODO: Why is this optional???
                CheckDigitConfigurationId = data.Id,
                CheckDigitConfigurationName = data.CheckDigitConfiguration?.Name,
                Id = data.Id,
                InputMask = data.InputMask,
                MaxLength = data.MaxLength,
                MinLength = data.MinLength,
                Name = data.Name,
                Regex = data.Regex
            };
        }
    }
}