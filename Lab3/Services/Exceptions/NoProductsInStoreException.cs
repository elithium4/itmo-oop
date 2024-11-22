namespace Lab3.Services.Exceptions
{
    public class NoProductsInStoreException: Exception
    {
        public NoProductsInStoreException(int storeId) : base($"No products in store with id {storeId}") { }
    }
}
