using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerPS
    {
        public List<PowerSupply> listPS = new List<PowerSupply>();
        public ContainerPS() { }

        public void Add(PowerSupply g)
        {
            listPS.Add(g);
        }
    }
}
