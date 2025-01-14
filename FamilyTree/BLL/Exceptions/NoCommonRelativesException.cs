namespace FamilyTree.BLL.Exceptions
{
    public class NoCommonRelativesException: Exception
    {
        public NoCommonRelativesException(int firstId, int secondId) : base($"No common relatives for {firstId} and {secondId}") { }
    }
}
