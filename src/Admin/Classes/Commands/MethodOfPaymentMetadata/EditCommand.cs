﻿using Admin.Models.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MethodOfPaymentMetadata
{
    public class EditCommand : BaseCommand<EditViewModel>
    {
        private readonly IMethodOfPaymentMetadataService _service;

        public EditCommand(ILog log
            , IMethodOfPaymentMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.MopMetadata()
            {
                Id = model.Id,
                MopCode = model.MopCode,
                MetadataKeyId = model.MetadataKeyId,
                Value = model.Value
            };

            var result = _service.Update(item);

            return new CommandResult(result);
        }
    }
}