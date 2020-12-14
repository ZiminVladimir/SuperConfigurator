using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerCase
    {
        public List<Case> listCase = new List<Case>();
        public ContainerCase() { }

        public void Add(Case g)
        {
            listCase.Add(g);
        }
    }
}
