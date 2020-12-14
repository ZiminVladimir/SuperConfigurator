using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class PowerSupply
    {
        public int Price;
        public string Name;
        public string Power;
        public string GPUPins;
        public string MBPins;
        public string CPUPins;

        public PowerSupply() { }
        public PowerSupply(int Pr, string N, string P, string GP, string MB, string CP)
        {
            Price = Pr;
            Name = N;
            Power = P;
            GPUPins = GP;
            MBPins = MB;
            CPUPins = CP;
        }
    }
}
