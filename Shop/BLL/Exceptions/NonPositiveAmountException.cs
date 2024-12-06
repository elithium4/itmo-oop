namespace BLL.Services.Exceptions
{
    public class NonPositiveAmountException: Exception
    {
        public NonPositiveAmountException() : base("Amount value must be positive") { }
    }
}
