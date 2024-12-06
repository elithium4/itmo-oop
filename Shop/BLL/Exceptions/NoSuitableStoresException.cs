namespace BLL.Services.Exceptions
{
    public class NoSuitableStoresException : Exception
    {
        public NoSuitableStoresException() : base("No suitable stores found") { }
    }
}
