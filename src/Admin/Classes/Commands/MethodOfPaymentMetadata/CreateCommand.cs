using Admin.Models.MethodOfPaymentMetadata;
using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.MethodOfPaymentMetadata
{
    public class CreateCommand : BaseCommand<EditViewModel>
    {
        private readonly IMethodOfPaymentMetadataService _service;

        public CreateCommand(ILog log
            , IMethodOfPaymentMetadataService service)
            : base(log)
        {
            _service = service;
        }

        protected override CommandResult OnExecute(EditViewModel model)
        {
            var item = new BusinessLogic.Entities.MopMetadata()
            {
                MopCode = model.MopCode,
                MopMetadataKeyId = model.MopMetadataKeyId,                
                Value = model.Value
            };

            var result = _service.Create(item);

            return new CommandResult(result);
        }
    }
}