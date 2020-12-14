using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerGPU
    {
        public List<GPU> listGPU = new List<GPU>();
        public ContainerGPU() { }

        public void Add(GPU g)
        {
            listGPU.Add(g);
        }
    }
}
