namespace BLL.Services.Exceptions
{
    public class NonPositivePriceException: Exception
    {
        public NonPositivePriceException() : base("Price value must be positive") { }
    }
}
