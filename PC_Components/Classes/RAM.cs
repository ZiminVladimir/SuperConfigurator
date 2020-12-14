using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class RAM
    {
        public int Price;
        public string Name;
        public string Type;
        public string Frequency;
        public string Volume;
        public string FormFactor;
        public string Number;

        public RAM() { }
        public RAM(int Pr, string N, string T, string F, string V, string Form, string Num)
        {
            Price = Pr;
            Name = N;
            Type = T;
            Frequency = F;
            Volume = V;
            FormFactor = Form;
            Number = Num;
        }
    }
}
