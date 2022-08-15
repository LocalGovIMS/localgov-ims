using BusinessLogic.Classes;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BusinessLogic.ImportProcessing
{
    public class AccountHolderImportProcessingStrategy : IImportProcessingStrategy
    {
        public int NumberOfSuccessfullyProcessedRows { get; private set; } = 0;

        private readonly IAccountHolderRepository _accountHolderRepository;
        private readonly ISecurityContext _securityContext;
        private List<string> _errors = new List<string>();

        public AccountHolderImportProcessingStrategy(
            IAccountHolderRepository accountHolderRepository,
            ISecurityContext securityContext)
        {
            _accountHolderRepository = accountHolderRepository ?? throw new ArgumentNullException("accountHolderRepository");
            _securityContext = securityContext ?? throw new ArgumentNullException("securityContext");
        }

        public void Process(ImportProcessingStrategyArgs args)
        {
            try
            {
                var accountHolders = args.ImportRows.Select(x => x.ToAccountHolder());

                BulkUpsert(accountHolders);
            }
            catch(Exception ex)
            {
                _errors.Add(ex.Message);
            }
            finally
            {
                args.Import.AddErrors(_errors);
            }
        }

        private void BulkUpsert(IEnumerable<Entities.AccountHolder> accountHolders)
        {
            var accountHoldersToCreate = _accountHolderRepository.BulkSelectNotExisting(accountHolders).ToList();
            var accountHoldersToUpdate = accountHolders.Except(accountHoldersToCreate, KeyBasedEqualityComparer<Entities.AccountHolder>.Create(x => x.AccountReference)).ToList();

            var batchSize = Convert.ToInt32(ConfigurationManager.AppSettings["Import.BatchSize"]);

            BulkInsert(accountHoldersToCreate, batchSize);

            BulkUpdate(accountHoldersToUpdate, batchSize);
        }

        private void BulkInsert(List<Entities.AccountHolder> accountHolders, int batchSize)
        {
            accountHolders.ForEach(x => { x.CreatedByUserId = _securityContext.UserId; x.CreatedAt = DateTime.Now; });

            foreach (var batch in accountHolders.Batch(batchSize))
            {
                _accountHolderRepository.BulkInsert(batch);
            }

            NumberOfSuccessfullyProcessedRows += accountHolders.Count();
        }

        private void BulkUpdate(List<Entities.AccountHolder> accountHolders, int batchSize)
        {
            accountHolders.ForEach(x => { x.UpdatedByUserId = _securityContext.UserId; x.UpdatedAt = DateTime.Now; });

            foreach (var batch in accountHolders.Batch(batchSize))
            {
                _accountHolderRepository.BulkUpdate(
                    batch,
                    new List<string>()
                    {
                        nameof(Entities.AccountHolder.CreatedByUserId),
                        nameof(Entities.AccountHolder.CreatedAt)
                    });
            }

            NumberOfSuccessfullyProcessedRows += accountHolders.Count();
        }
    }
}
