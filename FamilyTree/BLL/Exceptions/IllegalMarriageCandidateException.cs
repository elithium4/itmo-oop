
namespace FamilyTree.BLL.Exceptions
{
    public class IllegalMarriageCandidateException : Exception
    {
        public IllegalMarriageCandidateException() : base("Illegal marriage: candidates are relatives") { }
    }
}
