namespace FamilyTree.BLL.Exceptions
{
    internal class SamePersonRelationshipException: Exception
    {
        public SamePersonRelationshipException() : base("Can not assign a relationship with the person himself") { }
    }
}
