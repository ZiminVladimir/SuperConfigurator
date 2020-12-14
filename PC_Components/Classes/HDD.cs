using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class HDD
    {
        public int Price;
        public string Name;
        public string Volume;
        public string Revs;

        public HDD() { }
        public HDD(int Pr, string N, string Vol, string R)
        {
            Price = Pr;
            Name = N;
            Volume = Vol;
            Revs = R;
        }
    }
}
