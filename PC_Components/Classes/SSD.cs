using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class SSD
    {
        public int Price;
        public string Name;
        public string Volume;
        public bool M_2;

        public SSD() { }
        public SSD(int Pr, string N, string Vol, bool M)
        {
            Price = Pr;
            Name = N;
            Volume = Vol;
            M_2 = M;
        }
    }
}
