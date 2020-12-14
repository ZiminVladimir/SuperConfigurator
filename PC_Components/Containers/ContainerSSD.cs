using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class ContainerSSD
    {
        public List<SSD> listSSD = new List<SSD>();
        public ContainerSSD() { }

        public void Add(SSD g)
        {
            listSSD.Add(g);
        }
    }
}
