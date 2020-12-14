using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class CPU
    {
        public int Price;
        public string Name;
        public string Socket;
        public string Cores;
        public string Threads;
        public string Chipset;//for parasha(intel)
        public bool IGPU;
        public string MemVol;
        public string MemFreq;
        public string MemType;

        public CPU() { }
        public CPU(int Pr, string N, string S, string C, string T, string Ch, bool I, string MV, string MF, string MT)
        {
            Price = Pr;
            Name = N;
            Socket = S;
            Cores = C;
            Threads = T;
            Chipset = Ch;
            IGPU = I;
            MemVol = MV;
            MemFreq = MF;
            MemType = MT;
        }
    }
}
