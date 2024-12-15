using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL.Exceptions
{
    public class OnlyOneTreeAllowedException: Exception
    {
        public OnlyOneTreeAllowedException() : base("Only one tree allowed to be created in this app") { }
    }
}
