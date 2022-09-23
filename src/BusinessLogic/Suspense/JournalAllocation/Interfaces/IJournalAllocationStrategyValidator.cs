namespace BusinessLogic.Suspense.JournalAllocation
{
    public interface IJournalAllocationStrategyValidator
    {
        void Validate(JournalAllocationStrategyValidatorArgs args);
    }
}