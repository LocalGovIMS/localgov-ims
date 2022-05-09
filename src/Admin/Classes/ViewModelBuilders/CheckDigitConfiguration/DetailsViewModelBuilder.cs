using Admin.Models.CheckDigitConfiguration;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.CheckDigitConfiguration
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly ICheckDigitConfigurationService _service;

        public DetailsViewModelBuilder(ILog log
            , ICheckDigitConfigurationService checkDigitConfigurationService)
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
                ApplySubtraction = data.ApplySubtraction,
                Id = data.Id,
                Modulus = data.Modulus,
                Name = data.Name,
                ResultSubstitutions = data.ResultSubstitutions,
                SourceSubstitutions = data.SourceSubstitutions,
                Type = (CheckDigitType)data.Type,
                Weightings = data.Weightings,
            };
        }
    }
}