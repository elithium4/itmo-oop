using System.Xml.Linq;

namespace Lab3.Services.Exceptions
{
    public class ProductDoesNotExistsException: Exception
    {
        public ProductDoesNotExistsException(string productName) : base($"No product with name {productName}") { }
    }
}
