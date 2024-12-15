
namespace FamilyTree.BLL.Exceptions
{
    public class IllegalAgeDifferenceException : Exception
    {
        public IllegalAgeDifferenceException() : base("Parent can not be younger than child") { }
    }
}
