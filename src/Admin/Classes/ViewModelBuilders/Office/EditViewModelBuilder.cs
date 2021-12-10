using Admin.Models.Office;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Office
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, string>
    {
        private readonly IOfficeService _officeService;

        public EditViewModelBuilder(ILog log
            , IOfficeService officeService)
            : base(log)
        {
            _officeService = officeService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(string id)
        {
            var data = _officeService.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Code = data.OfficeCode;
            model.Name = data.Name;

            return model;
        }
    }
}