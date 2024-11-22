namespace Lab3.Services.Exceptions
{
    public class NotEnoughProduException: Exception
    {
        public NotEnoughProduException(int storeId, string product): base($"Not enough {product} in store with id {storeId}") { }
    }
}
