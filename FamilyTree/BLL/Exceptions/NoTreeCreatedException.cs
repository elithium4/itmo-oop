using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL.Exceptions
{
    public  class NoTreeCreatedException: Exception
    {
        public NoTreeCreatedException() : base("No tree created") { }
    }
}
