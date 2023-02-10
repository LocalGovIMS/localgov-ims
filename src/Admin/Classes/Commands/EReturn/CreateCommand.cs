using Admin.Models.EReturn;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.Commands.EReturn
{
    public class CreateCommand : BaseCommand<CreateViewModel>
    {
        private readonly IEReturnService _eReturnService;

        public CreateCommand(ILog log
            , IEReturnService eReturnService)
            : base(log)
        {
            _eReturnService = eReturnService ?? throw new ArgumentNullException("eReturnService");
        }

        protected override CommandResult OnExecute(CreateViewModel model)
        {
            var item = new BusinessLogic.Entities.EReturn()
            {
                ApprovedByUserId = null,
                StatusId = 1,
                TemplateId = model.TemplateId.Value,
                CreatedAt = DateTime.Now,
                TypeId = model.TypeId.Value
            };

            var result = _eReturnService.Create(item);

            return new CommandResult(result);
        }
    }
}