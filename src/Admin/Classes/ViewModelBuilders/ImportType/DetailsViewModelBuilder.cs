using Admin.Models.ImportType;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.ImportType
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, int>
    {
        private readonly IImportTypeService _ImportTypeService;

        public DetailsViewModelBuilder(ILog log
            , IImportTypeService ImportTypeService)
            : base(log)
        {
            _ImportTypeService = ImportTypeService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(int id)
        {
            var data = _ImportTypeService.Get(id);

            return new DetailsViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                DataType = (ImportDataTypeEnum)data.DataType,
                Description = data.Description,
                ExternalReference = data.ExternalReference,
                IsReversible = data.IsReversible
            };
        }
    }
}