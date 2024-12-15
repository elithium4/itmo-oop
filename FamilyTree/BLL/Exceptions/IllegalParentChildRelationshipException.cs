
namespace FamilyTree.BLL.Exceptions
{
    public class IllegalParentChildRelationshipException: Exception
    {
        public IllegalParentChildRelationshipException() : base("Illegal parent-child relationship: candidates are relatives") { }
    }
}
