namespace Lab3.Services.Exceptions
{
    public class NoStoreProductException: Exception
    {
        public NoStoreProductException(int storeId, string product) : base($"No product {product} in store with id {storeId}") { }
    }
}
