﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SuperParser;
using PC_Components;
using System.IO;
using Newtonsoft.Json;


namespace SuperConfigurator
{
    /// <summary>
    /// Interaction logic for Configurator.xaml
    /// </summary>
    public partial class Configurator : Window
    {
        BudgetsAndGigs bg;
        int gpu;
        int cpu;
        int gigs;
        bool memo = false;

        int budget = -1;
        bool two = false;
        List<CPU> cpus;
        List<GPU> gpus;
        List<HDD> hdds;
        List<MotherBoard> mbs;
        List<PowerSupply> pss;
        List<RAM> rams;
        List<SSD> ssds;
        List<Case> cs;

        GPU chosengpu = new GPU();
        CPU chosencpu = new CPU();
        MotherBoard chosenmb = new MotherBoard();
        RAM chosenram = new RAM();
        PowerSupply chosenps = new PowerSupply();
        SSD chosenssd = new SSD();
        Case chosencase = new Case();
        public Configurator()
        {
           using(var sr = new StreamReader(@"Resources\GPU.json"))
            {
                gpus = JsonConvert.DeserializeObject<List<GPU>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\MB.json"))
            {
                mbs = JsonConvert.DeserializeObject<List<MotherBoard>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\CPU.json"))
            {
                cpus = JsonConvert.DeserializeObject<List<CPU>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\RAM.json"))
            {
                rams = JsonConvert.DeserializeObject<List<RAM>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\PS.json"))
            {
                pss = JsonConvert.DeserializeObject<List<PowerSupply>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\Case.json"))
            {
                cs = JsonConvert.DeserializeObject<List<Case>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader(@"Resources\SSD.json"))
            {
                ssds = JsonConvert.DeserializeObject<List<SSD>>(sr.ReadToEnd());
            }
            InitializeComponent();
        }

        private void GPUBudgetCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox GPUBudgetCheckBox = (CheckBox)sender;
            if (GPUBudgetCheckBox.IsChecked == true) BudgetGPUTextBox.Visibility = Visibility.Visible;
            else if (GPUBudgetCheckBox.IsChecked == false) BudgetGPUTextBox.Visibility = Visibility.Hidden;
        }

        private void CPUBudgetCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox CPUBudgetCheckBox = (CheckBox)sender;
            if (CPUBudgetCheckBox.IsChecked == true) BudgetCPUTextBox.Visibility = Visibility.Visible;
            else if (CPUBudgetCheckBox.IsChecked == false) BudgetCPUTextBox.Visibility = Visibility.Hidden;
        }

        private void GigsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox GigsCheckBox = (CheckBox)sender;
            if (GigsCheckBox.IsChecked == true) GigsTextBox.Visibility = Visibility.Visible;
            else if (GigsCheckBox.IsChecked == false) GigsTextBox.Visibility = Visibility.Hidden;
        }

        private void BuildAllPC_Click(object sender, RoutedEventArgs e)
        {
            //if (BudgetTextBox.Text == "") MessageBox.Show("Введите ваш бюджет в рублях!");
            //else
            {
                try
                {
                    budget = int.Parse(BudgetTextBox.Text);
                    if (BudgetGPUTextBox.Visibility == Visibility.Visible) gpu = int.Parse(BudgetGPUTextBox.Text);
                    if (BudgetCPUTextBox.Visibility == Visibility.Visible) cpu = int.Parse(BudgetCPUTextBox.Text);
                    if (GigsTextBox.Visibility == Visibility.Visible) gigs = int.Parse(GigsTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите ваш бюджет в рублях!");
                }
            }

            bg = new BudgetsAndGigs(budget, gpu, cpu, gigs);
            int gpuprice = bg.GPUBudget;
            if (budget < 501000)
            {
                if (budget >= 30000)
                {
                    // Поиск видеокарты
                    int max = 0;
                    double tempprice = 0;
                    string nameg = "";
                    
                    if (budget > 45000)
                    {
                        if (gpuprice == 0)
                        {
                            tempprice = budget * 0.4;
                            memo = true;
                        }
                        else tempprice = bg.GPUBudget;
                        foreach (GPU g in gpus)
                        {
                            if (g.Name != "" && !g.Name.Contains("PNY") && !g.Name.Contains("Quadro"))
                            {
                                var m = g.Memory.Split();
                                int mem = 0;
                                if (m[0].Length < 3) mem = int.Parse(m[0]);
                                if (memo)
                                {
                                    if (g.Price > max && g.Price <= tempprice && mem > 4) { max = g.Price; nameg = g.Name; }
                                }
                                else
                                {
                                    if (g.Price > max && g.Price <= tempprice) { max = g.Price; nameg = g.Name; }
                                }
                            }
                        }
                        foreach (GPU g in gpus)
                        {
                            if (g.Price == max && g.Name == nameg)
                            {
                                chosengpu = g;
                            }
                        }
                    }
                    else if (budget <= 45000)
                    {
                        if (gpuprice == 0)
                        {
                            tempprice = budget * 0.35;
                        }
                        else tempprice = bg.GPUBudget;
                        foreach (GPU g in gpus)
                        {
                            if (g.Name.Length > 4 && !g.Name.Contains("PNY") && !g.Name.Contains("Quadro"))
                            {
                                if (g.Price > max && g.Price <= tempprice) { max = g.Price; nameg = g.Name; }
                            }
                        }
                        foreach (GPU g in gpus)
                        {
                            if (g.Price == max && g.Name == nameg)
                            {
                                chosengpu = g;
                            }
                        }
                    }

                    // Поиск процессора
                    max = 0;
                    int cores = 0;
                    int cpuprice = bg.CPUBudget;
                    tempprice = budget * 0.21;
                    foreach (CPU c in cpus)
                    {
                        if (cpuprice == 0)
                        {
                            tempprice = budget * 0.21;
                        }
                        else tempprice = bg.CPUBudget;
                        var cor = c.Cores.Split();
                        int co = int.Parse(cor[0]);
                        if (co < 17 && !c.Socket.Contains("SP3") && !c.Socket.Contains("2066") && !c.Name.Contains("Xeon") && !c.Name.Contains("Threadripper"))
                        {
                            if (c.Price <= tempprice && co > cores && c.Price >= tempprice / 3) { max = c.Price; cores = co; }
                        }
                    }
                    foreach (CPU c in cpus)
                    {
                        if (c.Price == max)
                        {
                            chosencpu = c;
                        }
                    }

                    // Поиск матери
                    max = 0;
                    string namemb = "";
                    if (chosencpu.Chipset == "AMD") //Амуде
                    {
                        if (budget < 60000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                string soc = "";
                                if (chosencpu.Socket.Split().Length == 1)
                                {
                                    soc = "AMD" + " " + chosencpu.Socket;
                                }
                                if (m.Socket == soc && m.Price < 5000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 60000 && budget < 81000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                string soc = "";
                                if (chosencpu.Socket.Split().Length == 1)
                                {
                                    soc = "AMD" + " " + chosencpu.Socket;
                                }
                                if (m.Socket == soc && m.Price < 6000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 81000 && budget < 101000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                string soc = "";
                                if (chosencpu.Socket.Split().Length == 1)
                                {
                                    soc = "AMD" + " " + chosencpu.Socket;
                                }
                                if (m.Socket == soc && m.Price < 7000 && m.Price > max)
                                {
                                    max = m.Price;
                                    namemb = m.Name;
                                }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 101000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                string soc = "";
                                if (chosencpu.Socket.Split().Length == 1)
                                {
                                    soc = "AMD" + " " + chosencpu.Socket;
                                }
                                if (m.Socket == soc && m.Price < 20000 && m.Price > max)
                                {
                                    max = m.Price;
                                    namemb = m.Name;
                                }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                    }
                    else if (chosencpu.Chipset != "AMD") //Интуль
                    {
                        if (budget <= 49000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 4500 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget < 60000 && budget > 49000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 5000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 60000 && budget < 81000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 6000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 81000 && budget < 101000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 7000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget >= 101000 && budget < 120000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 11000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                        else if (budget > 120000)
                        {
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Socket == chosencpu.Socket && m.Price < 20000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                            }
                            foreach (MotherBoard m in mbs)
                            {
                                if (m.Price == max && m.Name == namemb)
                                {
                                    chosenmb = m;
                                }
                            }
                        }
                    }
                    if (chosenmb.Name == null)
                    {
                        while (chosenmb.Name == null)
                        {
                            max = 0;
                            cores = 0;
                            tempprice = budget * 0.21;
                            string namecpu = "";
                            foreach (CPU c in cpus)
                            {
                                if (c.Socket.Equals(chosencpu.Socket))
                                {
                                    continue;
                                }
                                var cor = c.Cores.Split();
                                int co = int.Parse(cor[0]);
                                if (co < 17 && !c.Socket.Contains("SP3") && !c.Socket.Contains("2066") && !c.Name.Contains("Xeon") && !c.Name.Contains("Threadripper") && !c.Name.Equals(chosencpu.Name))
                                {
                                    if (c.Price <= tempprice && co > cores && c.Price >= tempprice / 3) { max = c.Price; cores = co; namecpu = c.Name; }
                                }
                            }
                            foreach (CPU c in cpus)
                            {
                                if (c.Price == max && c.Name == namecpu)
                                {
                                    chosencpu = c;
                                }
                            }

                            max = 0;
                            namemb = "";
                            if (chosencpu.Chipset == "AMD") //Амуде
                            {
                                if (budget < 60000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        string soc = "";
                                        if (chosencpu.Socket.Split().Length == 1)
                                        {
                                            soc = "AMD" + " " + chosencpu.Socket;
                                        }
                                        if (m.Socket == soc && m.Price < 5000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 60000 && budget < 81000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        string soc = "";
                                        if (chosencpu.Socket.Split().Length == 1)
                                        {
                                            soc = "AMD" + " " + chosencpu.Socket;
                                        }
                                        if (m.Socket == soc && m.Price < 6000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 81000 && budget < 101000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        string soc = "";
                                        if (chosencpu.Socket.Split().Length == 1)
                                        {
                                            soc = "AMD" + " " + chosencpu.Socket;
                                        }
                                        if (m.Socket == soc && m.Price < 7000 && m.Price > max)
                                        {
                                            max = m.Price;
                                            namemb = m.Name;
                                        }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 101000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        string soc = "";
                                        if (chosencpu.Socket.Split().Length == 1)
                                        {
                                            soc = "AMD" + " " + chosencpu.Socket;
                                        }
                                        if (m.Socket == soc && m.Price < 20000 && m.Price > max)
                                        {
                                            max = m.Price;
                                            namemb = m.Name;
                                        }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                            }
                            else if (chosencpu.Chipset != "AMD") //Интуль
                            {
                                if (budget <= 49000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 4500 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget < 60000 && budget > 49000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 5000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 60000 && budget < 81000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 6000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 81000 && budget < 101000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 7000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget >= 101000 && budget < 120000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 11000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                                else if (budget > 120000)
                                {
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Socket == chosencpu.Socket && m.Price < 20000 && m.Price > max) { max = m.Price; namemb = m.Name; }
                                    }
                                    foreach (MotherBoard m in mbs)
                                    {
                                        if (m.Price == max && m.Name == namemb)
                                        {
                                            chosenmb = m;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Выбираем память
                    foreach(var r in rams)
                    {
                        if (r.Number.Contains("2") && r.FormFactor == "DIMM" && r.Price < 4000 && int.Parse(r.Volume[0].ToString()) == 8 && r.Price < 9999)
                        {

                        }
                    }
                    max = 0;
                    int max2 = 9999;
                    string namer = "";
                    if (budget < 38000) // продумать ситуацию с комплектами по 1, 2 и 4 плашки
                    {
                        foreach (RAM r in rams)
                        {
                            if (r.Price < 3700)
                            {
                                var v = r.Volume.Split();
                                string volume = v[0];
                                if (r.Number.Contains("1") && r.FormFactor == "DIMM" && r.Price < 1500 && int.Parse(volume) == 4 && r.Price < max2 && r.Type == chosencpu.MemType)
                                {
                                    max2 = r.Price;
                                    namer = r.Name;
                                    two = false;
                                }
                                else if (r.Number.Contains("2") && r.FormFactor == "DIMM" && r.Price < 3700 && int.Parse(volume) == 8 && r.Price < max2 && r.Type == chosencpu.MemType)
                                {
                                    max2 = r.Price;
                                    namer = r.Name;
                                    two = true;
                                }
                            }
                        }
                        foreach (RAM r in rams)
                        {
                            if (r.Price == max2 && r.Name == namer)
                            {
                                chosenram = r;
                            }
                        }
                    }
                    else if (budget >= 38000 && budget <= 50000)
                    {
                        foreach (RAM r in rams)
                        {
                            var v = r.Volume.Split();
                            string volume = v[0];
                            if (r.Number.Contains("1") && r.FormFactor == "DIMM" && r.Price < 2600 && int.Parse(volume) == 8 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = false;
                            }
                            else if (r.Number.Contains("2") && r.FormFactor == "DIMM" && r.Price < 5500 && int.Parse(volume) == 16 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = true;
                            }
                        }
                        foreach (RAM r in rams)
                        {
                            if (r.Price == max2 && r.Name == namer)
                            {
                                chosenram = r;
                            }
                        }
                    }
                    else if (budget > 50000 && budget < 100000)
                    {
                        foreach (RAM r in rams)
                        {
                            var v = r.Volume.Split();
                            string volume = v[0];
                            if (r.Number.Contains("1") && r.FormFactor == "DIMM" && r.Price < 3000 && int.Parse(volume) == 8 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = false;
                            }
                            else if (r.Number.Contains("2") && r.FormFactor == "DIMM" && r.Price < 6300 && int.Parse(volume) == 16 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = true;
                            }
                        }
                        foreach (RAM r in rams)
                        {
                            if (r.Price == max2 && r.Name == namer)
                            {
                                chosenram = r;
                            }
                        }
                    }
                    else if (budget >= 100000)
                    {
                        foreach (RAM r in rams)
                        {
                            var v = r.Volume.Split();
                            string volume = v[0];
                            if (r.Number.Contains("1") && r.FormFactor == "DIMM" && r.Price < 6000 && int.Parse(volume) == 16 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = false;
                            }
                            else if (r.Number.Contains("2") && r.FormFactor == "DIMM" && r.Price < 13000 && int.Parse(volume) == 32 && r.Price < max2 && r.Type == chosencpu.MemType)
                            {
                                max2 = r.Price;
                                namer = r.Name;
                                two = true;
                            }
                        }
                        foreach (RAM r in rams)
                        {
                            if (r.Price == max2 && r.Name == namer)
                            {
                                chosenram = r;
                            }
                        }
                    }

                    // Выбираем БП
                    max = 0;
                    string nameps = "";
                    bool six = false;
                    bool eight = false;
                    bool eightplus6 = false;
                    bool eightplus8 = false;
                    bool eightplus8plus8 = false;
                    if (chosengpu.AddPowerPin == "6 pin") six = true;
                    else if (chosengpu.AddPowerPin == "8 pin") eight = true;
                    else if (chosengpu.AddPowerPin == "8 + 6 pin" || chosengpu.AddPowerPin == "6 + 8 pin") eightplus6 = true;
                    else if (chosengpu.AddPowerPin == "8 + 8 pin") eightplus8 = true;
                    else if (chosengpu.AddPowerPin == "8 + 8 + 8 pin") eightplus8plus8 = true;
                    string mbpins = chosenmb.MainPins[0].ToString() + chosenmb.MainPins[1].ToString();
                    string cpupins = chosenmb.CPUPins[0].ToString();

                    if (budget < 51000)
                    {
                        foreach (PowerSupply p in pss)
                        {
                            string RecPowSup;
                            var r = new string[10];
                            if (chosengpu.RecomPowSup == null) RecPowSup = "300";
                            else
                            {
                                r = chosengpu.RecomPowSup.Split();
                                RecPowSup = r[0];
                            }
                            var po = p.Power.Split();
                            string power = po[0];
                            if (p.Price > max && p.Price < 3000 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == mbpins && p.CPUPins == cpupins)
                            {
                                int Gpu6;
                                int Gpu8;
                                if (p.GPUPins6 == "") Gpu6 = 0;
                                else Gpu6 = int.Parse(p.GPUPins6[0].ToString());
                                if (p.GPUPins8 == "") Gpu8 = 0;
                                else Gpu8 = int.Parse(p.GPUPins8[0].ToString());
                                if ((chosengpu.AddPowerPin == null)) { max = p.Price; nameps = p.Name; }
                                else if ((six && Gpu6 >= 1) || (six && Gpu8 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eight && Gpu8 >= 1) { max = p.Price; nameps = p.Name; }
                                else if ((eightplus6 && Gpu8 >= 2) || (eightplus6 && Gpu8 >= 1 && Gpu6 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8 && Gpu8 >= 2) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8plus8 && Gpu8 >= 3) { max = p.Price; nameps = p.Name; }
                            }
                        }
                        foreach (PowerSupply p in pss)
                        {
                            if (p.Price == max && p.Name == nameps)
                            {
                                chosenps = p;
                            }
                        }
                    }
                    else if (budget >= 51000 && budget < 71000)
                    {
                        foreach (PowerSupply p in pss)
                        {
                            string RecPowSup;
                            var r = new string[10];
                            if (chosengpu.RecomPowSup == null) RecPowSup = "300";
                            else
                            {
                                r = chosengpu.RecomPowSup.Split();
                                RecPowSup = r[0];
                            }
                            var po = p.Power.Split();
                            string power = po[0];
                            if (p.Price < 4200 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == mbpins && p.CPUPins == cpupins)
                            {
                                int Gpu6;
                                int Gpu8;
                                if (p.GPUPins6 == "") Gpu6 = 0;
                                else Gpu6 = int.Parse(p.GPUPins6[0].ToString());
                                if (p.GPUPins8 == "") Gpu8 = 0;
                                else Gpu8 = int.Parse(p.GPUPins8[0].ToString());
                                if ((chosengpu.AddPowerPin == null)) { max = p.Price; nameps = p.Name; }
                                else if ((six && Gpu6 >= 1) || (six && Gpu8 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eight && Gpu8 >= 1) { max = p.Price; nameps = p.Name; }
                                else if ((eightplus6 && Gpu8 >= 2) || (eightplus6 && Gpu8 >= 1 && Gpu6 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8 && Gpu8 >= 2) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8plus8 && Gpu8 >= 3) { max = p.Price; nameps = p.Name; }
                            }
                        }
                        foreach (PowerSupply p in pss)
                        {
                            if (p.Price == max && p.Name == nameps)
                            {
                                chosenps = p;
                            }
                        }
                    }
                    else if (budget >= 71000 && budget < 91000)
                    {
                        foreach (PowerSupply p in pss)
                        {
                            string RecPowSup;
                            var r = new string[10];
                            if (chosengpu.RecomPowSup == null) RecPowSup = "300";
                            else
                            {
                                r = chosengpu.RecomPowSup.Split();
                                RecPowSup = r[0];
                            }
                            var po = p.Power.Split();
                            string power = po[0];
                            if (p.Price < 5200 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == mbpins && p.CPUPins == cpupins)
                            {
                                int Gpu6;
                                int Gpu8;
                                if (p.GPUPins6 == "") Gpu6 = 0;
                                else Gpu6 = int.Parse(p.GPUPins6[0].ToString());
                                if (p.GPUPins8 == "") Gpu8 = 0;
                                else Gpu8 = int.Parse(p.GPUPins8[0].ToString());
                                if ((chosengpu.AddPowerPin == null)) { max = p.Price; nameps = p.Name; }
                                else if ((six && Gpu6 >= 1) || (six && Gpu8 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eight && Gpu8 >= 1) { max = p.Price; nameps = p.Name; }
                                else if ((eightplus6 && Gpu8 >= 2) || (eightplus6 && Gpu8 >= 1 && Gpu6 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8 && Gpu8 >= 2) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8plus8 && Gpu8 >= 3) { max = p.Price; nameps = p.Name; }
                            }
                        }
                        foreach (PowerSupply p in pss)
                        {
                            if (p.Price == max && p.Name == nameps)
                            {
                                chosenps = p;
                            }
                        }
                    }
                    else if (budget >= 91000 && budget < 150000)
                    {
                        foreach (PowerSupply p in pss)
                        {
                            string RecPowSup;
                            var r = new string[10];
                            if (chosengpu.RecomPowSup == null) RecPowSup = "300";
                            else
                            {
                                r = chosengpu.RecomPowSup.Split();
                                RecPowSup = r[0];
                            }
                            var po = p.Power.Split();
                            string power = po[0];
                            if (p.Price < 6100 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == mbpins && p.CPUPins == cpupins)
                            {
                                int Gpu6;
                                int Gpu8;
                                if (p.GPUPins6 == "") Gpu6 = 0;
                                else Gpu6 = int.Parse(p.GPUPins6[0].ToString());
                                if (p.GPUPins8 == "") Gpu8 = 0;
                                else Gpu8 = int.Parse(p.GPUPins8[0].ToString());
                                if ((chosengpu.AddPowerPin == null)) { max = p.Price; nameps = p.Name; }
                                else if ((six && Gpu6 >= 1) || (six && Gpu8 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eight && Gpu8 >= 1) { max = p.Price; nameps = p.Name; }
                                else if ((eightplus6 && Gpu8 >= 2) || (eightplus6 && Gpu8 >= 1 && Gpu6 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8 && Gpu8 >= 2) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8plus8 && Gpu8 >= 3) { max = p.Price; nameps = p.Name; }
                            }
                        }
                        foreach (PowerSupply p in pss)
                        {
                            if (p.Price == max && p.Name == nameps)
                            {
                                chosenps = p;
                            }
                        }
                    }
                    else if (budget >= 150000)
                    {
                        foreach (PowerSupply p in pss)
                        {
                            string RecPowSup;
                            var r = new string[10];
                            if (chosengpu.RecomPowSup == null) RecPowSup = "300";
                            else
                            {
                                r = chosengpu.RecomPowSup.Split();
                                RecPowSup = r[0];
                            }
                            var po = p.Power.Split();
                            string power = po[0];
                            if (p.Price < 10000 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == mbpins && p.CPUPins == cpupins)
                            {
                                int Gpu6;
                                int Gpu8;
                                if (p.GPUPins6 == "") Gpu6 = 0;
                                else Gpu6 = int.Parse(p.GPUPins6[0].ToString());
                                if (p.GPUPins8 == "") Gpu8 = 0;
                                else Gpu8 = int.Parse(p.GPUPins8[0].ToString());
                                if ((chosengpu.AddPowerPin == null)) { max = p.Price; nameps = p.Name; }
                                else if ((six && Gpu6 >= 1) || (six && Gpu8 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eight && Gpu8 >= 1) { max = p.Price; nameps = p.Name; }
                                else if ((eightplus6 && Gpu8 >= 2) || (eightplus6 && Gpu8 >= 1 && Gpu6 >= 1)) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8 && Gpu8 >= 2) { max = p.Price; nameps = p.Name; }
                                else if (eightplus8plus8 && Gpu8 >= 3) { max = p.Price; nameps = p.Name; }
                            }
                        }
                        foreach (PowerSupply p in pss)
                        {
                            if (p.Price == max && p.Name == nameps)
                            {
                                chosenps = p;
                            }
                        }
                    }

                    //Выбираем SSD
                    max = 9999;

                    if (budget < 50000)
                    {
                        foreach (SSD s in ssds)
                        {
                            var v = s.Volume.Split();
                            int vol = int.Parse(v[0]);
                            if ((vol == 240 || vol == 256 || vol == 250) && s.Price < max) max = s.Price;
                        }
                        foreach (SSD s in ssds)
                        {
                            if (s.Price == max)
                            {
                                chosenssd = s;
                            }
                        }
                    }
                    else if (budget >= 50000 && budget < 80000)
                    {
                        max = 9999;
                        foreach (SSD s in ssds)
                        {
                            var v = s.Volume.Split();
                            int vol = int.Parse(v[0]);
                            if ((vol == 480 || vol == 500 || vol == 512) && s.Price < max) max = s.Price;
                        }
                        foreach (SSD s in ssds)
                        {
                            if (s.Price == max)
                            {
                                chosenssd = s;
                            }
                        }
                    }
                    else if (budget >= 80000 && budget < 200000)
                    {
                        //int max1 = 10000;
                        max = 10000;
                        foreach (SSD s in ssds)
                        {
                            var v = s.Volume.Split();
                            int vol = int.Parse(v[0]);
                            if ((vol == 1000 || vol == 960 || vol == 1024) && s.Price < max) max = s.Price;
                        }
                        foreach (SSD s in ssds)
                        {
                            if (s.Price == max)
                            {
                                chosenssd = s;
                            }
                        }
                    }
                    else if (budget >= 200000)
                    {
                        int max1 = 15000;
                        max = 0;
                        foreach (SSD s in ssds)
                        {
                            var v = s.Volume.Split();
                            int vol = int.Parse(v[0]);
                            if ((vol == 1000 || vol == 960 || vol == 1024) && s.Price < max1 && s.Price > max && s.Price < max1) max = s.Price;
                        }
                        foreach (SSD s in ssds)
                        {
                            if (s.Price == max)
                            {
                                chosenssd = s;
                            }
                        }
                    }

                    //Выбираем корпус
                    max = budget - (chosencpu.Price + chosengpu.Price + chosenmb.Price + chosenps.Price + chosenram.Price + chosenram.Price + chosenssd.Price);
                    int max0 = 0;
                    var l = chosengpu.Length.Split();
                    string length = l[0];
                    string name = "";
                    foreach (Case c in cs)
                    {
                        var le = c.MaxGPULength;
                        if (le == null || le == "") continue;
                        string lenCase = le[0].ToString() + le[1].ToString() + le[2].ToString();
                        if (c.Price < 3100 && c.Price > max0 && int.Parse(length) <= int.Parse(lenCase) && !c.Name.Contains("Рекомендуем")) { max0 = c.Price; name = c.Name; }
                    }
                    foreach (Case c in cs)
                    {
                        if (c.Price == max0 && c.Name == name)
                        {
                            chosencase = c;
                        }
                    }
                    max = chosencpu.Price + chosengpu.Price + chosenmb.Price + chosenps.Price + chosenram.Price + chosenram.Price + chosenssd.Price + chosencase.Price;
                    int ost = budget - max;
                    if (ost > 500)
                    {
                        if (budget > 45000)
                        {
                            tempprice = budget * 0.4;
                            tempprice += ost;

                            foreach (GPU g in gpus)
                            {
                                if (g.Name != "" && !g.Name.Contains("PNY") && !g.Name.Contains("Quadro"))
                                {
                                    var m = g.Memory.Split();
                                    int mem = 0;
                                    if (m[0].Length < 3) mem = int.Parse(m[0]);
                                    if (g.Price > max && g.Price <= tempprice && mem > 4) { max = g.Price; nameg = g.Name; }
                                }
                            }
                            foreach (GPU g in gpus)
                            {
                                if (g.Price == max && g.Name == nameg)
                                {
                                    chosengpu = g;
                                }
                            }
                        }
                        else if (budget <= 44999)
                        {
                            tempprice = budget * 0.35;
                            tempprice += ost;
                            foreach (GPU g in gpus)
                            {
                                if (g.Name.Length > 4 && !g.Name.Contains("PNY") && !g.Name.Contains("Quadro"))
                                {
                                    if (g.Price > max && g.Price <= tempprice) { max = g.Price; nameg = g.Name; }
                                }
                            }
                            foreach (GPU g in gpus)
                            {
                                if (g.Price == max && g.Name == nameg)
                                {
                                    chosengpu = g;
                                }
                            }
                        }
                    }
                    Write();
                }
                else if (budget < 30000) MessageBox.Show("Наименьший бюджет для сборки ПК в нашем сервисе — 30.000 рублей.");
                //if (budget == 0) MessageBox.Show("Введите ваш бюджет в рублях!");
            }
            else if (budget >= 501000)
            {
                MessageBox.Show("Макисмальный бюджет для сборки — 500.000 рублей.");
            }
            gpu = 0;
            cpu = 0;
            gigs = 0;
        }
        private void Write()
        {
            int price; 
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Видеокарта:" + chosengpu.Name + " ——— " + chosengpu.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Процессор:" + chosencpu.Name + " ——— " + chosencpu.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Материнская плата:" + chosenmb.Name + " ——— " + chosenmb.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            if (two)
            {
                price = chosenram.Price;
                var v = chosenram.Volume.Split();
                int vol = int.Parse(v[0]) / 2;
                sb.AppendFormat("Оперативная память:" + chosenram.Name + " (" + vol + "ГБ" + "+" + vol + "ГБ" + ") ——— " + price.ToString() + "(2 плашки)");
                sb.AppendLine();
                sb.AppendLine();
            }
            else
            {
                price = chosenram.Price * 2;
                sb.AppendFormat("Оперативная память:" + chosenram.Name + " (" + chosenram.Volume + "+" + chosenram.Volume + ") ——— " + price.ToString() + "(2 плашки)");
                sb.AppendLine();
                sb.AppendLine();
            }
            sb.AppendFormat("Блок питания:" + chosenps.Name + " " + chosenps.Power + " ——— " + chosenps.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Твердотельный накопитель:" + chosenssd.Name + " " + chosenssd.Volume + " ——— " + chosenssd.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("Корпус:" + chosencase.Name + " ——— " + chosencase.Price.ToString());
            sb.AppendLine();
            sb.AppendLine();
            int all = chosencpu.Price + chosengpu.Price + chosenmb.Price + chosenps.Price + price + chosenssd.Price + chosencase.Price;
            int ostatok = budget - all;
            sb.AppendFormat("Остаток по бюджету: " + ostatok.ToString());
            FinalComponentsLabel.Content = sb.ToString();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Благодарим за использование нашего сервиса! Если у вас есть предложения по добавлению функционала или приложение работает некорректно — направляйте запросы на электронную почту kb11so@yandex.ru.");
            Close();
        }

        
    }
}
