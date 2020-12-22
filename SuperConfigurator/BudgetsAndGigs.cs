using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperConfigurator
{
    public class BudgetsAndGigs
    {
        public int AllBudget;
        public int GPUBudget;
        public int CPUBudget;
        public int Gigs;

        public BudgetsAndGigs() { }

        public BudgetsAndGigs(int all, int gpu, int cpu, int g)
        {
            AllBudget = all;
            GPUBudget = gpu;
            CPUBudget = cpu;
            Gigs = g;
        }
    }
}
