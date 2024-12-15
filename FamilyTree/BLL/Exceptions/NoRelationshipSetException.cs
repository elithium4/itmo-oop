using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class NoRelationshipSetException: Exception
    {
        public NoRelationshipSetException(int firstPersonId, int secondPersonId) : base($"People with id {firstPersonId} and {secondPersonId} have no relationship") { }
    }
}
