using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerHDD
    {
        public List<HDD> listHDD = new List<HDD>();
        public ContainerHDD() { }

        public void Add(HDD g)
        {
            listHDD.Add(g);
        }
    }
}
