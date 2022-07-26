using BusinessLogic.Entities;
using DataAccess.Interfaces;
using DataAccess.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace DataAccess.Classes
{
    public class AuditLogger : IAuditLogger
    {
        private readonly Dictionary<string, int> _typeMapping = new Dictionary<string, int>() {
            { typeof(FundGroup).ToString(), 1 },
            { typeof(FundGroupFund).ToString(), 2 },
            { typeof(Fund).ToString(), 3 },
            { typeof(Mop).ToString(), 4 },
            { typeof(FundMessage).ToString(), 6 },
            { typeof(TransactionNotification).ToString(), 7 },
            { typeof(PendingTransaction).ToString(), 8 },
            { typeof(ProcessedTransaction).ToString(), 9 },
            { typeof(UserFundGroup).ToString(), 10 },
            { typeof(UserRole).ToString(), 11 },
            { typeof(User).ToString(), 12 },
            { typeof(Vat).ToString(), 13 },
            { typeof(Template).ToString(), 14 },
            { typeof(Role).ToString(), 15 },
            { typeof(EReturn).ToString(), 16 },
            { typeof(EReturnCash).ToString(), 17 },
            { typeof(EReturnCheque).ToString(), 18 },
            { typeof(Suspense).ToString(), 19 },
            { typeof(SuspenseProcessedTransaction).ToString(), 20 },
            { typeof(TemplateRow).ToString(), 21 },
            { typeof(UserTemplate).ToString(), 22 },
            { typeof(EmailLog).ToString(), 23 },
            { typeof(ActivityLog).ToString(), 24 },
            { typeof(SystemMessage).ToString(), 25 },
            { typeof(FileImport).ToString(), 26 },
            { typeof(FileImportRow).ToString(), 27 },
            { typeof(ImportProcessingRule).ToString(), 28 },
            { typeof(ImportProcessingRuleAction).ToString(), 29 },
            { typeof(ImportProcessingRuleCondition).ToString(), 30 },
            { typeof(ImportProcessingRuleField).ToString(), 31 },
            { typeof(ImportProcessingRuleOperator).ToString(), 32 },
            { typeof(PaymentIntegration).ToString(), 33 },
            { typeof(ScheduleLog).ToString(), 34 },
            { typeof(TransactionStatus).ToString(), 35 },
            { typeof(UserMethodOfPayment).ToString(), 36 },
            { typeof(SuspenseNote).ToString(), 37 },
            { typeof(FundMetadata).ToString(), 38 },
            { typeof(MopMetadata).ToString(), 39 },
            { typeof(VatMetadata).ToString(), 40 },
            { typeof(Office).ToString(), 41 },
            { typeof(AccountHolder).ToString(), 42 },
            { typeof(AccountReferenceValidator).ToString(), 43 },
            { typeof(EReturnStatus).ToString(), 44 },
            { typeof(EReturnType).ToString(), 45 },
            { typeof(FundMessageMetadata).ToString(), 46 },
            { typeof(Import).ToString(), 47 },
            { typeof(ImportEventLog).ToString(), 48 },
            { typeof(ImportRow).ToString(), 49 },
            { typeof(ImportStatusHistory).ToString(), 50 },
            { typeof(ImportType).ToString(), 51 },
            { typeof(ImportTypeImportProcessingRule).ToString(), 52 },
            { typeof(CheckDigitConfiguration).ToString(), 53 }
        };

        private readonly Dictionary<string, Func<object, string>> _idMapping = new Dictionary<string, Func<object, string>>() {
            { typeof(FundGroup).ToString(), (item) => { return ((FundGroup)item).FundGroupId.ToString(); } },
            { typeof(FundGroupFund).ToString(), (item) => { return ((FundGroupFund)item).FundGroupFundId.ToString(); } },
            { typeof(Fund).ToString(), (item) => { return ((Fund)item).FundCode; } },
            { typeof(Mop).ToString(), (item) => { return ((Mop)item).MopCode; } },
            { typeof(FundMessage).ToString(), (item) => { return ((FundMessage)item).Id.ToString(); } },
            { typeof(TransactionNotification).ToString(), (item) => { return ((TransactionNotification)item).Id.ToString(); } },
            { typeof(PendingTransaction).ToString(), (item) => { return ((PendingTransaction)item).Id.ToString(); } },
            { typeof(ProcessedTransaction).ToString(), (item) => { return ((ProcessedTransaction)item).Id.ToString(); } },
            { typeof(UserFundGroup).ToString(), (item) => { return ((UserFundGroup)item).UserFundGroupId.ToString(); } },
            { typeof(UserRole).ToString(), (item) => { return ((UserRole)item).UserRoleId.ToString(); } },
            { typeof(User).ToString(), (item) => { return ((User)item).UserId.ToString(); } },
            { typeof(Vat).ToString(), (item) => { return ((Vat)item).VatCode; } },
            { typeof(Template).ToString(), (item) => { return ((Template)item).Id.ToString(); } },
            { typeof(Role).ToString(), (item) => { return ((Role)item).RoleId.ToString(); } },
            { typeof(EReturn).ToString(), (item) => { return ((EReturn)item).Id.ToString(); } },
            { typeof(EReturnCash).ToString(), (item) => { return ((EReturnCash)item).Id.ToString(); } },
            { typeof(EReturnCheque).ToString(), (item) => { return ((EReturnCheque)item).Id.ToString(); } },
            { typeof(Suspense).ToString(), (item) => { return ((Suspense)item).Id.ToString(); } },
            { typeof(SuspenseProcessedTransaction).ToString(), (item) => { return ((SuspenseProcessedTransaction)item).Id.ToString(); } },
            { typeof(TemplateRow).ToString(), (item) => { return ((TemplateRow)item).Id.ToString(); } },
            { typeof(UserTemplate).ToString(), (item) => { return ((UserTemplate)item).UserTemplateId.ToString(); } },
            { typeof(EmailLog).ToString(), (item) => { return ((EmailLog)item).Id.ToString(); } },
            { typeof(ActivityLog).ToString(), (item) => { return ((ActivityLog)item).Id.ToString(); } },
            { typeof(SystemMessage).ToString(), (item) => { return ((SystemMessage)item).Id.ToString(); } },
            { typeof(FileImport).ToString(), (item) => { return ((FileImport)item).Id.ToString(); } },
            { typeof(FileImportRow).ToString(), (item) => { return ((FileImportRow)item).Id.ToString(); } },
            { typeof(ImportProcessingRule).ToString(), (item) => { return ((ImportProcessingRule)item).Id.ToString(); } },
            { typeof(ImportProcessingRuleAction).ToString(), (item) => { return ((ImportProcessingRuleAction)item).Id.ToString(); } },
            { typeof(ImportProcessingRuleCondition).ToString(), (item) => { return ((ImportProcessingRuleCondition)item).Id.ToString(); } },
            { typeof(ImportProcessingRuleField).ToString(), (item) => { return ((ImportProcessingRuleField)item).Id.ToString(); } },
            { typeof(ImportProcessingRuleOperator).ToString(), (item) => { return ((ImportProcessingRuleOperator)item).Id.ToString(); } },
            { typeof(PaymentIntegration).ToString(), (item) => { return ((PaymentIntegration)item).Id.ToString(); } },
            { typeof(ScheduleLog).ToString(), (item) => { return ((ScheduleLog)item).Id.ToString(); } },
            { typeof(TransactionStatus).ToString(), (item) => { return ((TransactionStatus)item).Id.ToString(); } },
            { typeof(UserMethodOfPayment).ToString(), (item) => { return ((UserMethodOfPayment)item).Id.ToString(); } },
            { typeof(SuspenseNote).ToString(), (item) => { return ((SuspenseNote)item).Id.ToString(); } },
            { typeof(FundMetadata).ToString(), (item) => { return ((FundMetadata)item).Id.ToString(); } },
            { typeof(MopMetadata).ToString(), (item) => { return ((MopMetadata)item).Id.ToString(); } },
            { typeof(VatMetadata).ToString(), (item) => { return ((VatMetadata)item).Id.ToString(); } },
            { typeof(Office).ToString(), (item) => { return ((Office)item).OfficeCode.ToString(); } },
            { typeof(AccountHolder).ToString(), (item) => { return ((AccountHolder)item).AccountReference.ToString(); } },
            { typeof(AccountReferenceValidator).ToString(), (item) => { return ((AccountReferenceValidator)item).Id.ToString(); } },
            { typeof(CheckDigitConfiguration).ToString(), (item) => { return ((CheckDigitConfiguration)item).Id.ToString(); } },
            { typeof(EReturnStatus).ToString(), (item) => { return ((EReturnStatus)item).Id.ToString(); } },
            { typeof(EReturnType).ToString(), (item) => { return ((EReturnType)item).Id.ToString(); } },
            { typeof(FundMessageMetadata).ToString(), (item) => { return ((FundMessageMetadata)item).Id.ToString(); } },
            { typeof(Import).ToString(), (item) => { return ((Import)item).Id.ToString(); } },
            { typeof(ImportEventLog).ToString(), (item) => { return ((ImportEventLog)item).Id.ToString(); } },
            { typeof(ImportRow).ToString(), (item) => { return ((ImportRow)item).Id.ToString(); } },
            { typeof(ImportStatusHistory).ToString(), (item) => { return ((ImportStatusHistory)item).Id.ToString(); } },
            { typeof(ImportType).ToString(), (item) => { return ((ImportType)item).Id.ToString(); } },
            { typeof(ImportTypeImportProcessingRule).ToString(), (item) => { return ((ImportTypeImportProcessingRule)item).Id.ToString(); } }
        };

        public void CreateAudit(IncomeDbContext context, int userId)
        {
            var _groupdId = Guid.NewGuid();

            var addedAuditedEntities = context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);

            var modifiedAuditedEntities = context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);

            var deletedAuditedEntities = context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Deleted)
                .Select(p => p.Entity);

            var now = DateTime.Now;

            foreach (var myObject in addedAuditedEntities)
            {
                if (!_typeMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_typeMapping entry is missing for entity: " + myObject.ToString());
                if (!_idMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_idMapping entry is missing for entity: " + myObject.ToString());

                context.ActivityLogs.Add(new ActivityLog
                {
                    UserId = userId,
                    ObjectType = _typeMapping[myObject.ToString()],
                    ObjectId = _idMapping[myObject.ToString()](myObject),
                    Description = string.Format("Added a: {0}", myObject.ToString()),
                    CreatedAt = DateTime.Now,
                    GroupId = _groupdId
                });
            }

            foreach (var myObject in modifiedAuditedEntities)
            {
                if (!_typeMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_typeMapping entry is missing for entity: " + myObject.ToString());
                if (!_idMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_idMapping entry is missing for entity: " + myObject.ToString());

                var myObjectState = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(myObject);
                var modifiedProperties = myObjectState.GetModifiedProperties();

                foreach (var propName in modifiedProperties)
                {
                    context.ActivityLogs.Add(new ActivityLog
                    {
                        UserId = userId,
                        ObjectType = _typeMapping[myObject.ToString()],
                        ObjectId = _idMapping[myObject.ToString()](myObject),
                        Description = string.Format("Property {0} changed from {1} to {2}",
                            propName,
                            myObjectState.OriginalValues[propName],
                            myObjectState.CurrentValues[propName]),
                        CreatedAt = DateTime.Now,
                        GroupId = _groupdId
                    });
                }
            }

            foreach (var myObject in deletedAuditedEntities)
            {
                if (!_typeMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_typeMapping entry is missing for entity: " + myObject.ToString());
                if (!_idMapping.ContainsKey(myObject.ToString())) throw new KeyNotFoundException("An AuditLogger::_idMapping entry is missing for entity: " + myObject.ToString());

                context.ActivityLogs.Add(new ActivityLog
                {
                    UserId = userId,
                    ObjectType = _typeMapping[myObject.ToString()],
                    ObjectId = _idMapping[myObject.ToString()](myObject),
                    Description = string.Format("Deleted a: {0}", myObject.ToString()),
                    CreatedAt = DateTime.Now,
                    GroupId = _groupdId
                });
            }
        }
    }

    class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);

            if (prop.PropertyType.IsClass &&
                prop.PropertyType != typeof(string))
            {
                prop.ShouldSerialize = obj => false;
            }

            return prop;
        }
    }
}
