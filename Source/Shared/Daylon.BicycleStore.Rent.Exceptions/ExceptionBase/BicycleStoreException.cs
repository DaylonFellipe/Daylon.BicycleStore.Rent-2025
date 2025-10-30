namespace Daylon.BicycleStore.Rent.Exceptions.ExceptionBase
{
    public class BicycleStoreException : SystemException
    {
        public BicycleStoreException()
        {
        }

        public BicycleStoreException(string message) : base(message)
        {
        }

        public BicycleStoreException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
