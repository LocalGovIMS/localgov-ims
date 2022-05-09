using Admin.Models.UserTemplate;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Mvc;

namespace Admin.Classes.ViewModelBuilders.UserTemplate
{
    public class EditViewModelBuilder : BaseViewModelBuilder<EditViewModel, int>
    {
        private readonly IUserService _userService;
        private readonly ITemplateService _templateService;
        private readonly IUserTemplateService _userTemplateService;

        public EditViewModelBuilder(ILog log
            , IUserService userService
            , ITemplateService templateService
            , IUserTemplateService userTemplateService)
            : base(log)
        {
            _userService = userService;
            _templateService = templateService;
            _userTemplateService = userTemplateService;
        }

        protected override EditViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override EditViewModel OnBuild(int id)
        {
            var user = _userService.GetUser(id);
            var existingItems = _userTemplateService.GetByUserId(id);

            var model = new EditViewModel();

            if (user == null) return model;
            if (existingItems == null) return model;

            model.UserId = user.UserId;
            model.UserName = user.UserName;
            model.Templates = GetTemplates(existingItems);

            return model;
        }

        private ICollection<CheckBoxListItem> GetTemplates(List<BusinessLogic.Entities.UserTemplate> existingItems)
        {
            var allItems = _templateService.GetAllTemplates().OrderBy(x => x.Name);

            return allItems.Select(x => new CheckBoxListItem()
            {
                Id = x.Id.ToString(),
                Text = x.Name,
                IsChecked = existingItems.Any(y => y.TemplateId == x.Id)
            })
                .ToList();
        }
    }
}