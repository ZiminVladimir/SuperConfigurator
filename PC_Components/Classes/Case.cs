using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class Case
    {
        public int Price;
        public string Name;
        public string MaxGPULength;

        public Case() { }

        public Case(int P, string N, string M)
        {
            Price = P;
            Name = N;
            MaxGPULength = M;
        }
    }
}
