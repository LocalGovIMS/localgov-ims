using Admin.Models.Template;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Template
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly ITemplateService _templateService;
        private readonly IVatService _vatService;

        public EditViewModelBuilder(ILog log
            , ITemplateService templateService
            , IVatService vatService
            ) : base(log)
        {
            _templateService = templateService;
            _vatService = vatService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _templateService.GetTemplate(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.Id = data.Id;
            model.Name = data.Name;
            model.AllowCheque = data.Cheque;
            model.AllowCash = data.Cash;
            model.AllowPdq = data.Pdq;
            model.TemplateRows = data.TemplateRows.ToList();

            model.VatList = _vatService.GetAllCodes().ToList();

            return model;
        }

        protected override EditViewModel OnRebuild(EditViewModel model)
        {
            model.VatList = _vatService.GetAllCodes().ToList();

            return model;
        }
    }
}