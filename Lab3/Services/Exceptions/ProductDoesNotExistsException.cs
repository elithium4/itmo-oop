namespace Lab3.Services.Exceptions
{
    public class ProductDoesNotExistsException: Exception
    {
        public ProductDoesNotExistsException(string message) : base(message) { }
    }
}
