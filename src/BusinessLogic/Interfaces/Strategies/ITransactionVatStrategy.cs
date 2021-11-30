using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Strategies
{
    public interface ITransactionVatStrategy
    {
        void AddVatToTransaction(ProcessedTransaction transaction);
    }
}
