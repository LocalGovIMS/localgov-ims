using Admin.Models.EReturnTemplate;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplate
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IEReturnTemplateService _service;

        public DetailsViewModelBuilder(ILog log
            , IEReturnTemplateService service)
            : base(log)
        {
            _service = service;
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
                Id = data.Id,
                Name = data.Name,
                AllowCash = data.Cash,
                AllowCheque = data.Cheque,
                AllowPdq = data.Pdq
            };
        }
    }
}