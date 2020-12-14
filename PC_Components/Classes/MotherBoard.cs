using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Components
{
    public class MotherBoard
    {
        public int Price;
        public string Name;
        public string Socket;
        public string Chipset;
        public string MemType;
        public string MemFreq;
        public string MemVol;
        public bool M_2;
        public string MainPins;
        public string CPUPins;

        public MotherBoard() { }
        public MotherBoard(int Pr, string N, string S, string Ch, string MT, string MF, string MV, bool M, string MP, string CP)
        {
            Price = Pr;
            Name = N;
            Socket = S;
            Chipset = Ch;
            MemType = MT;
            MemFreq = MF;
            MemVol = MV;
            M_2 = M;
            MainPins = MP;
            CPUPins = CP;
        }
    }
}
