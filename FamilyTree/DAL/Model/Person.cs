
namespace FamilyTree.DAL.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }

        public int? SpouseId { get; set; }
        public List<int> Parents {  get; set; }
        public List<int> Children { get; set; }

        public Person()
        {
            Parents = new List<int>();
            Children = new List<int>();
        }
    }
}
