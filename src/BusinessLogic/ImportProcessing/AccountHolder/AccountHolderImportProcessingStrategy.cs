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
            var accountHolderToCreate = args.Row.ToAccountHolder();

            var result = _accountHolderService.Create(new Services.CreateAccountHolderArgs()
            {
                SaveChanges = false,
                AccountHolder = accountHolderToCreate
            });

            if (!result.Success)
                throw new ImportProcessingException(result.Error);
        }
    }
}
