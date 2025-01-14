namespace FamilyTree.BLL.DTO
{
    public class PersonDTO: CreatePersonDTO
    {
        public int Id { get; set; }
        public int? SpouseId { get; set; }
    }
}
