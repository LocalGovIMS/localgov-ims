using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Services;
using System;

namespace BusinessLogic.ImportProcessing
{
    public class AccountHolderImportProcessingStrategy : IImportProcessingStrategy
    {
        private readonly IAccountHolderService _accountHolderService;

        public AccountHolderImportProcessingStrategy(IAccountHolderService accountHolderService)
        {
            _accountHolderService = accountHolderService ?? throw new ArgumentNullException("accountHolderService");
        }

        public void Process(ImportProcessingStrategyArgs args)
        {
            var accountHolderToUpsert = args.Row.ToAccountHolder();

            var updateResult = _accountHolderService.Update(new Services.UpdateAccountHolderArgs()
            {
                SaveChanges = false,
                AccountHolder = accountHolderToUpsert
            });

            if (updateResult.Success) return;

            var createResult = _accountHolderService.Create(new Services.CreateAccountHolderArgs()
            {
                SaveChanges = false,
                AccountHolder = accountHolderToUpsert
            });

            if (!createResult.Success)
                throw new ImportProcessingException(createResult.Error);
        }
    }
}
