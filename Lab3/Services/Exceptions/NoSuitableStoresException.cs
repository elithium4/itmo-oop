namespace Lab3.Services.Exceptions
{
    public class NoSuitableStoresException : Exception
    {
        public NoSuitableStoresException() : base("No suitable stores found") { }
    }
}
