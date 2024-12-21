using AutoMapper;
using FamilyTree.DAL.Model;
using FamilyTree.BLL.DTO;

namespace FamilyTree.BLL.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<CreatePersonDTO, Person>();
        }
    }
}
