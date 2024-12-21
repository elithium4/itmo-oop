namespace FamilyTree.BLL.Exceptions
{
    public class BadAgeDifferenceException: Exception
    {
        public BadAgeDifferenceException() : base("Ancestor was not born yet") { }
    }
}
