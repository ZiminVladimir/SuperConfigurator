using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerCPU
    {
        public List<CPU> listCPU = new List<CPU>();
        public ContainerCPU() { }

        public void Add(CPU g)
        {
            listCPU.Add(g);
        }
    }
}
