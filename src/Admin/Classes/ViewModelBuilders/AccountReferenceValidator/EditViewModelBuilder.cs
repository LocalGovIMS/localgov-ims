using Admin.Models.AccountReferenceValidator;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.AccountReferenceValidator
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IAccountReferenceValidatorService _service;
        private readonly ICheckDigitConfigurationService _checkDigitConfigurationService;

        public EditViewModelBuilder(ILog log
            , IAccountReferenceValidatorService service
            , ICheckDigitConfigurationService checkDigitconfigurationService)
            : base(log)
        {
            _service = service;
            _checkDigitConfigurationService = checkDigitconfigurationService;
        }

        protected override EditViewModel OnBuild()
        {
            return new EditViewModel
            {
                CharacterTypes = GetCharacterTypes(),
                CheckDigitConfigurations = GetCheckDigitConfigurations()
            }; ;
        }

        protected override EditViewModel OnBuild(int id)
        {
            var data = _service.Get(id);
            var model = new EditViewModel();

            if (data == null) return model;

            model.CharacterType = data.CharacterType;
            model.CheckDigitConfigurationId = data.Id;
            model.Id = data.Id;
            model.InputMask = data.InputMask;
            model.MaxLength = data.MaxLength;
            model.MinLength = data.MinLength;
            model.Name = data.Name;
            model.Regex = data.Regex;

            model.CharacterTypes = GetCharacterTypes();
            model.CheckDigitConfigurations = GetCheckDigitConfigurations();

            return model;
        }

        // TODO: What if we add a new option? We have to manually update this???
        private SelectList GetCharacterTypes()
        {
            return new SelectList(EnumHelper.GetSelectList(typeof(BusinessLogic.Enums.CharacterType)), false);
        }

        private SelectList GetCheckDigitConfigurations()
        {
            var selectListItems = new List<SelectListItem>();
            var items = _checkDigitConfigurationService.GetAll()
                .OrderBy(x => x.Name);

            foreach (var item in items)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                });
            }

            return new SelectList(selectListItems, false);
        }
    }
}