using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Strategies;

namespace BusinessLogic.Classes.Strategies
{
    public class TransactionVatStrategy : ITransactionVatStrategy
    {
        private readonly IFundRepository _fundRepository;
        private readonly IVatRepository _vatRepository;

        public TransactionVatStrategy(
            IFundRepository fundRepository,
            IVatRepository vatRepository)
        {
            _fundRepository = fundRepository;
            _vatRepository = vatRepository;
        }

        //  HIGH: Maybe this should just take fund code, vat code and amount and return
        //  a vat object and an amount object? That would make it re-usable for pending transactions        
        public void AddVatToTransaction(ProcessedTransaction transaction)
        {
            var targetVat = _vatRepository.GetVatByVatCode(transaction.VatCode);
            var targetFund = _fundRepository.GetByFundCode(transaction.FundCode);

            var vatCode = targetFund.VatCode;
            var vatRate = decimal.ToSingle(targetFund.Vat.Percentage ?? 0);

            if (targetFund.VatOverride)
            {
                if (targetVat.VatCode != targetFund.VatCode)
                {
                    vatCode = targetVat.VatCode;
                    vatRate = decimal.ToSingle(targetVat.Percentage ?? 0);
                }
            }

            transaction.VatCode = vatCode;
            transaction.VatRate = vatRate;

            if (transaction.Amount != null)
            {
                transaction.VatAmount =
                    decimal.Round(
                        transaction.Amount.Value -
                        transaction.Amount.Value / (decimal)(1 + (vatRate)), 2);
            }
        }
    }
}
