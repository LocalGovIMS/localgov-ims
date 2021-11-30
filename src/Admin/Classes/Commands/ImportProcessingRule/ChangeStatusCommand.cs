using BusinessLogic.Interfaces.Services;
using log4net;

namespace Admin.Classes.Commands.ImportProcessingRule
{
    public class ChangeStatusCommand : BaseCommand<ChangeStatusCommandArgs>
    {
        private readonly IImportProcessingRuleService _service;

        public ChangeStatusCommand(ILog log
            , IImportProcessingRuleService importProcessingRuleService)
            : base(log)
        {
            _service = importProcessingRuleService;
        }

        protected override CommandResult OnExecute(ChangeStatusCommandArgs args)
        {
            var rule = _service.Get(args.ImportProcessingRuleId);

            rule.Disabled = args.IsDisabled;

            var result = _service.Update(rule);

            return new CommandResult(result);
        }
    }

    public class ChangeStatusCommandArgs
    {
        public int ImportProcessingRuleId { get; set; }
        public bool IsDisabled { get; set; }
    }
}