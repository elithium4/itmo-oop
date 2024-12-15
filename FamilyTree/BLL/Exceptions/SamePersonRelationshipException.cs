using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class SamePersonRelationshipException: Exception
    {
        public SamePersonRelationshipException() : base("Can not assign a relationship with the person himself") { }
    }
}
