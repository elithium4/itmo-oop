using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL.Exceptions
{
    public class BadAgeDifferenceException: Exception
    {
        public BadAgeDifferenceException() : base("Ancestor was not born yet") { }
    }
}
