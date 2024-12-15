using FamilyTree.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace FamilyTree.BLL.DTO
{
    public class CreatePersonDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Surname must be at least 3 characters long")]
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
    }
}
