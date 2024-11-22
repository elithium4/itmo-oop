namespace Lab3.Services.Exceptions
{
    public class ProductAlreadyExistsException: Exception
    {
        public ProductAlreadyExistsException(string productName): base($"Product with name {productName} already exists") { }
    }
}
