using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerMB
    {
        public List<MotherBoard> listMB = new List<MotherBoard>();
        public ContainerMB () { }

        public void Add(MotherBoard g)
        {
            listMB.Add(g);
        }
    }
}
