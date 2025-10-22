namespace Daylon.BicycleStore.Rent.Exceptions.ExceptionBase
{
    public class ErrorOnValidationExeption : BicycleStoreException
    {
        public IList<string> ErrorMessages { get; }

        public ErrorOnValidationExeption(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
