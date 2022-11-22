using Admin.Models.EReturnTemplateRow;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.EReturnTemplateRow
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IEReturnTemplateRowService _service;

        public DetailsViewModelBuilder(ILog log
            , IEReturnTemplateRowService service)
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
                EReturnTemplateId = data.TemplateId,
                Reference = data.Reference,
                ReferenceOverride = data.ReferenceOverride,
                VatCode = data.VatCode,
                VatOverride = data.VatOverride,
                Description = data.Description,
                DescriptionOverride = data.DescriptionOverride

            };
        }
    }
}