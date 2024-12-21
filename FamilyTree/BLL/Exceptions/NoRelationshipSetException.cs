namespace FamilyTree.BLL.Exceptions
{
    internal class NoRelationshipSetException: Exception
    {
        public NoRelationshipSetException(int firstPersonId, int secondPersonId) : base($"People with id {firstPersonId} and {secondPersonId} have no relationship") { }
    }
}
