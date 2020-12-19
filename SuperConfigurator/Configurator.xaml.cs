using System;
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
        int budget = -1;
        List<CPU> cpus;
        List<GPU> gpus;
        List<HDD> hdds;
        List<MotherBoard> mbs;
        List<PowerSupply> pss;
        List<RAM> rams;
        List<SSD> ssds;
        List<Case> cs;
        public Configurator()
        {
           using(var sr = new StreamReader("GPU.json"))
            {
                gpus = JsonConvert.DeserializeObject<List<GPU>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("MB.json"))
            {
                mbs = JsonConvert.DeserializeObject<List<MotherBoard>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("CPU.json"))
            {
                cpus = JsonConvert.DeserializeObject<List<CPU>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("RAM.json"))
            {
                rams = JsonConvert.DeserializeObject<List<RAM>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("PS.json"))
            {
                pss = JsonConvert.DeserializeObject<List<PowerSupply>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("Case.json"))
            {
                cs = JsonConvert.DeserializeObject<List<Case>>(sr.ReadToEnd());
            }
            using (var sr = new StreamReader("SSD.json"))
            {
                ssds = JsonConvert.DeserializeObject<List<SSD>>(sr.ReadToEnd());
            }
            InitializeComponent();
        }

        private void BuildAllPC_Click(object sender, RoutedEventArgs e)
        {
            if (BudgetTextBox.Text == "") MessageBox.Show("Введите ваш бюджет в рублях!");
            else
            {
                try
                {
                    budget = int.Parse(BudgetTextBox.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите ваш бюджет в рублях!");
                }
            }

            if (budget >= 30000)
            {
                StringBuilder sb = new StringBuilder();
                GPU chosengpu = new GPU();
                CPU chosencpu = new CPU();
                MotherBoard chosenmb = new MotherBoard();
                RAM chosenram = new RAM();
                PowerSupply chosenps = new PowerSupply();
                SSD chosenssd = new SSD();
                Case chosencase = new Case();

                // Поиск видеокарты
                double max = 0;
                double tempprice = 0;
                if (budget > 45000)
                {
                    tempprice = budget * 0.4;
                    
                    foreach (GPU g in gpus)
                    {
                        if (g.Price > max && g.Price <= tempprice) max = g.Price;
                    }
                    foreach (GPU g in gpus)
                    {
                        if (g.Price == max)
                        {
                            sb.AppendFormat("Видеокарта:" + g.Name + " ——— " + g.Price.ToString());
                            sb.AppendLine();
                            chosengpu = g;
                        }
                    }
                }
                else if (budget <= 44999)
                {
                    tempprice = budget * 0.35;
                    foreach (GPU g in gpus)
                    {
                        if (g.Price > max && g.Price <= tempprice) max = g.Price;
                    }
                    foreach (GPU g in gpus)
                    {
                        if (g.Price == max)
                        {
                            sb.AppendFormat("Видеокарта:" + g.Name + " ——— " + g.Price.ToString());
                            sb.AppendLine();
                            chosengpu = g;
                        }
                    }
                }

                // Поиск процессора
                max = 0;
                int cores = 0;
                tempprice = budget * 0.21;
                foreach (CPU c in cpus)
                {
                    var cor = c.Cores.Split();
                    int co = int.Parse(cor[0]);
                    if (c.Price <= tempprice && co > cores && c.Price >= tempprice/2) { max = c.Price; cores = co; }
                }
                foreach (CPU c in cpus)
                {
                    if (c.Price == max)
                    {
                        sb.AppendFormat("Процессор:" + c.Name + " ——— " + c.Price.ToString());
                        sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
                                chosenmb = m;
                            }
                        }
                    }
                    else if (budget >= 101000)
                    {
                        foreach (MotherBoard m in mbs)
                        {string soc = "";
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
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
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
                                chosenmb = m;
                            }
                        }
                    }
                }

                foreach(var m in mbs)
                {
                    if (m.Socket == chosencpu.Socket && m.Price < 4500)
                    {

                    }
                }
                // Выбираем память
                max = 0;
                int max2 = 9999;
                if (budget < 38000) // продумать ситуацию с комплектами по 1, 2 и 4 плашки
                {
                    foreach (RAM r in rams)
                    {
                        var v = r.Volume.Split();
                        string volume = v[0];
                        var frRam = r.Frequency.Split();
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split();
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split();
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 1500 && int.Parse(volume) == 4 && r.Price < max2 && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max2 = r.Price;
                    }
                    foreach (RAM r in rams)
                    {
                        if (r.Price == max2)
                        {
                            int price = r.Price * 2;
                            sb.AppendFormat("Оперативная память:" + r.Name + " " + r.Volume + " ——— " + price.ToString() + "(2 плашки)");
                            sb.AppendLine();
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
                        var frRam = r.Frequency.Split();
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split();
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split();
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 2600 && int.Parse(volume) == 8 && r.Price < max2 && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max2 = r.Price;
                    }
                    foreach (RAM r in rams)
                    {
                        if (r.Price == max2)
                        {
                            int price = r.Price * 2;
                            sb.AppendFormat("Оперативная память:" + r.Name + " " + r.Volume + " ——— " + price.ToString() + "(2 плашки)");
                            sb.AppendLine();
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
                        var frRam = r.Frequency.Split();
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split();
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split();
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 3000 && int.Parse(volume) == 8 && r.Price > max && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max = r.Price;
                    }
                    foreach (RAM r in rams)
                    {
                        if (r.Price == max)
                        {
                            int price = r.Price * 2;
                            sb.AppendFormat("Оперативная память:" + r.Name + " " + r.Volume + " ——— " + price.ToString() + "(2 плашки)");
                            sb.AppendLine();
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
                        var frRam = r.Frequency.Split();
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split();
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split();
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 6100 && int.Parse(volume) == 16 && r.Price > max && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max = r.Price;
                    }
                    foreach (RAM r in rams)
                    {
                        if (r.Price == max)
                        {
                            int price = r.Price * 2;
                            sb.AppendFormat("Оперативная память:" + r.Name + " " + r.Volume + " ——— " + price.ToString() + "(2 плашки)");
                            sb.AppendLine();
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
                else if (chosengpu.AddPowerPin == "8 + 6 pin") eightplus6 = true;
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
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            sb.AppendLine();
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 51000 && budget < 71000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split();
                        string RecPowSup = r[0];
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
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            sb.AppendLine();
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 71000 && budget < 91000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split();
                        string RecPowSup = r[0];
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
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            sb.AppendLine();
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 91000 && budget < 150000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split();
                        string RecPowSup = r[0];
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
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            sb.AppendLine();
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 150000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split();
                        string RecPowSup = r[0];
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
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            sb.AppendLine();
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
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + " ——— " + s.Price.ToString());
                            sb.AppendLine();
                            chosenssd = s;
                        }
                    }
                }
                else if (budget >= 50000 && budget < 80000)
                {
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
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + " ——— " + s.Price.ToString());
                            sb.AppendLine();
                            chosenssd = s;
                        }
                    }
                }
                else if (budget >= 80000 && budget < 200000)
                {
                    int max1 = 10000;
                    max = 0;
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
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + " ——— " + s.Price.ToString());
                            sb.AppendLine();
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
                        if (s.Volume == "1000 ГБ" && s.Price < max1 && s.Price > max) max = s.Price;
                    }
                    foreach (SSD s in ssds)
                    {
                        if (s.Price == max)
                        {
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + " ——— " + s.Price.ToString());
                            sb.AppendLine();
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
                    string lenCase = le[0].ToString()+le[1].ToString() + le[2].ToString();
                    if (c.Price < max && c.Price > max0 && int.Parse(length) <= int.Parse(lenCase) && !c.Name.Contains("Рекомендуем")) { max0 = c.Price; name = c.Name; }
                }
                foreach (Case c in cs)
                {
                    if (c.Price == max0 && c.Name == name)
                    {
                        sb.AppendFormat("Корпус:" + c.Name + " ——— " + c.Price.ToString());
                        sb.AppendLine();
                        chosencase = c;
                    }
                }
                FinalComponentsLabel.Content = sb.ToString();
            }
            else if (budget < 30000 && budget > -1) MessageBox.Show("К сожалению, наименьший бюджет для сборки ПК в нашем сервисе — 30.000 рублей.");
            else MessageBox.Show("Введите ваш бюджет в рублях!");
            //if (budget == 0) MessageBox.Show("Введите ваш бюджет в рублях!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Благодарим за использование нашего сервиса! Если у вас есть предложения по добавлению функционала или приложение работает некорректно, то направляйте запросы на электронную почту kb11so@yandex.ru.");
            Close();
        }
    }
}
