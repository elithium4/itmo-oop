using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL.DTO
{
    public class TreePersonDTO
    {
        public PersonDTO Person;
        public List<PersonDTO> Children;
    }
}
