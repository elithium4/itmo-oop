namespace BLL.Services.Exceptions
{
    public class ProductDoesNotExistsException: Exception
    {
        public ProductDoesNotExistsException(string productName) : base($"No product with name {productName}") { }
    }
}
