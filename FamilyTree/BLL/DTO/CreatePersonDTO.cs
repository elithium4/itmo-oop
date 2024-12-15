using FamilyTree.DAL.Model;

namespace FamilyTree.BLL.DTO
{
    public class CreatePersonDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
