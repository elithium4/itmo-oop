namespace Lab3.Services.Exceptions
{
    public class StoreDoesNotExistException: Exception
    {
        public StoreDoesNotExistException(string message) : base(message) { }
    }
}
