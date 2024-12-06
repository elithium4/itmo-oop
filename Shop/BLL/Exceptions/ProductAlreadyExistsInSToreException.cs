namespace BLL.Services.Exceptions
{
    public class ProductAlreadyExistsInSToreException: Exception
    {
        public ProductAlreadyExistsInSToreException(int storeId, string productName): base($"Store with id {storeId} already contains product {productName}") { }
    }
}
