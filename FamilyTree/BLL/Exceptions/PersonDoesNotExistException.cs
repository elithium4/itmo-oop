namespace FamilyTree.BLL.Exceptions
{
    public class PersonDoesNotExistException : Exception
    {
        public PersonDoesNotExistException(int id) : base($"Person with id {id} does not exist") { }
    }
}
