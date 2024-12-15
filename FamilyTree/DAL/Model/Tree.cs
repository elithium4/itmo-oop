using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.DAL.Model
{
    public class Tree
    {
        public int Id { get; set; }
        public List<int> Members { get; set; }
    }
}
