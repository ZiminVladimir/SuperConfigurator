using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class GPU
    {
        public int Price;
        public string Name;
        public string Memory;
        public string Power;
        public string RecomPowSup;
        public string Length;
        public string AddPowerPin;

        public GPU() { }
        public GPU(int Pr, string N, string M, string P, string R, string L, string A)
        {
            Price = Pr;
            Name = N;
            Memory = M;
            Power = P;
            RecomPowSup = R;
            Length = L;
            AddPowerPin = A;
        }
    }
}
