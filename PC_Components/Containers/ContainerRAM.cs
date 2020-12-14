using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerRAM
    {
        public List<RAM> listRAM = new List<RAM>();
        public ContainerRAM() { }

        public void Add(RAM g)
        {
            listRAM.Add(g);
        }
    }
}
