namespace BusinessLogic.Interfaces.Validators
{
    public interface IValidator<T>
    {
        IValidator<T> SetNext(IValidator<T> handler);

        void Validate(T args);
    }
}
