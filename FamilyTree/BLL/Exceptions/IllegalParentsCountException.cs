
namespace FamilyTree.BLL.Exceptions
{
    public class IllegalParentsCountException: Exception
    {
        public IllegalParentsCountException() : base("Child can not have more than two parents") { } 
    }
}
