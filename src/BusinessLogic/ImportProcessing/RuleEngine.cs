using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class RuleEngine : IRuleEngine
    {
        private readonly ILog _log;
        private readonly IImportProcessingRuleService _importProcessingRuleService;
        private readonly IOperations _operations;

        private IList<ImportProcessingRule> _rules;

        private ProcessedTransaction _transaction;
        private bool _groupResult = false;
        private bool _processRuleActions = false;

        public RuleEngine(ILog log,
            IImportProcessingRuleService importProcessingRuleService,
            IOperations operations)
        {
            _log = log;
            _importProcessingRuleService = importProcessingRuleService;
            _operations = operations;

            LoadRules();
        }

        private void LoadRules()
        {
            _rules = _importProcessingRuleService.GetAll(false);
        }

        public ProcessedTransaction Process(ProcessedTransaction transaction)
        {
            _transaction = transaction;

            foreach (var rule in _rules)
            {
                InitialiseRuleCheck();
                CheckTheRule(rule);
                ProcessActions(rule.Actions);
            }

            return transaction;
        }

        private void InitialiseRuleCheck()
        {
            _processRuleActions = false;
        }

        private void CheckTheRule(ImportProcessingRule rule)
        {
            foreach (var group in rule.Groups())
            {
                InitialiseGroupProcessing();
                ProcessGroup(rule.GroupConditions(group));

                // If we know we need to process the actions there is no point processing
                // any more groups as the groups are combined using OR logic
                if (_processRuleActions) break;
            }
        }

        private void InitialiseGroupProcessing()
        {
            _groupResult = false;
        }

        private void ProcessGroup(IOrderedEnumerable<ImportProcessingRuleCondition> groupConditions)
        {
            foreach (var condition in groupConditions)
            {
                ProcessCondition(condition);
            }

            if (_groupResult) _processRuleActions = true;
        }

        private void ProcessCondition(ImportProcessingRuleCondition condition)
        {
            var operation = GetOperation(condition);
            var operationArgs = GetOperationArgs(condition);

            var result = operation(operationArgs);

            ProcessResult(result, condition);
        }

        private Func<OperationArgs, bool> GetOperation(ImportProcessingRuleCondition condition)
        {
            IDictionary<OperationType, Func<OperationArgs, bool>> operations = new Dictionary<OperationType, Func<OperationArgs, bool>>();

            switch (condition.Field.Type)
            {
                case FieldType.Text:
                    operations = _operations.TextOperators;
                    break;
                case FieldType.Decimal:
                    operations = _operations.TextOperators;
                    break;
                default:
                    throw new NotImplementedException($"The field type '{condition.Field.Type}' has not been implemented");
            }

            return operations[(OperationType)condition.ImportProcessingRuleOperatorId];
        }

        private OperationArgs GetOperationArgs(ImportProcessingRuleCondition condition)
        {
            return new OperationArgs()
            {
                FieldValue = _transaction.GetPropertyValue(condition.Field.Name),
                Value = condition.Value
            };
        }

        private void ProcessResult(bool result, ImportProcessingRuleCondition condition)
        {
            switch (condition.LogicalOperator)
            {
                case null:
                    _groupResult = result;
                    break;
                case LogicalOperationType.And:
                    _groupResult = _groupResult && result;
                    break;
                case LogicalOperationType.Or:
                    _groupResult = _groupResult || result;
                    break;
                default:
                    throw new NotImplementedException($"The logical operation '{condition.LogicalOperator}' is not yet implemented");
            }
        }

        private void ProcessActions(ICollection<ImportProcessingRuleAction> actions)
        {
            if (!_processRuleActions) return;

            foreach (var action in actions)
            {
                _transaction.SetPropertyValue<string>(action.Field.Name, action.Value);
            }
        }
    }

    public class OperationArgs
    {
        public dynamic FieldValue { get; set; }
        public dynamic Value { get; set; }
    }
}
