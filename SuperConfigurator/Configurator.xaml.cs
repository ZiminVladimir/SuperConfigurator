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

namespace SuperConfigurator
{
    /// <summary>
    /// Interaction logic for Configurator.xaml
    /// </summary>
    public partial class Configurator : Window
    {
        public Configurator()
        {
            cpus = new List<CPU>()
            {
                new CPU(10050, "AMD Ryzen 5 2600", "AM4", "6 cores", "12 threads", "AMD", false, "64 ГБ", "2933 МГц", "ddr4")
            };
            gpus = new List<GPU>()
            {
                new GPU(22200, "NVIDIA GeForce GTX 1660 Ti", "6 ГБ", "120 Вт", "450 Вт", "170 мм", "8")
            };
            hdds = new List<HDD>()
            {
                new HDD(2980, "WD Blue", "1000 ГБ", "7200 Об/мин")
            };
            mbs = new List<MotherBoard>()
            {
                new MotherBoard(4290, "Gigabyte a320-s2h v2", "AM4", "AMD", "ddr4", "3200 МГц", "32 ГБ", true, "24-контактный", "8-контактное")
            };
            pss = new List<PowerSupply>()
            {
                new PowerSupply(2700, "FSP PNR-I", "500 Вт", "6+2", "24", "4")
            };
            rams = new List<RAM>()
            {
                new RAM(2512, "Hynix", "ddr4", "2400 МГц", "8 ГБ", "DIMM", "1 шт")
            };
            ssds = new List<SSD>()
            {
                new SSD(3990, "kingston a400", "480 ГБ", false)
            };
            cs = new List<Case>()
            {
                new Case(1220, "ExeGate AB-222", "235 мм")
            };
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
                double tempprice = budget * 0.45;
                double max = 0;
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

                // Поиск процессора
                max = 0;
                tempprice = budget * 0.21;
                foreach (CPU c in cpus)
                {
                    if (c.Price > max && c.Price <= tempprice) max = c.Price;
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
                if (chosencpu.Chipset == "AMD") //Амуде
                {
                    if (budget < 60000)
                    {
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Chipset == "AMD" && m.Price < 5000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == "AMD" && m.Price < 6000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == "AMD" && m.Price < 7000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                        {
                            if (m.Chipset == "AMD" && m.Price < 20000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                    if (budget < 60000)
                    {
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Chipset == chosencpu.Chipset && m.Price < 5000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == chosencpu.Chipset && m.Price < 6000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == chosencpu.Chipset && m.Price < 7000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == chosencpu.Chipset && m.Price < 11000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
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
                            if (m.Chipset == chosencpu.Chipset && m.Price < 20000 && m.Price > max) max = m.Price;
                        }
                        foreach (MotherBoard m in mbs)
                        {
                            if (m.Price == max)
                            {
                                sb.AppendFormat("Материнская плата:" + m.Name + " ——— " + m.Price.ToString());
                                sb.AppendLine();
                                chosenmb = m;
                            }
                        }
                    }
                }

                // Выбираем память
                max = 0;
                if (budget < 38000) // продумать ситуацию с комплектами по 1, 2 и 4 плашки
                {
                    foreach (RAM r in rams)
                    {
                        var v = r.Volume.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string volume = v[0];
                        var frRam = r.Frequency.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 1500 && int.Parse(volume) == 4 && r.Price > max && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max = r.Price;
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
                else if (budget >= 38000 && budget <= 50000)
                {
                    foreach (RAM r in rams)
                    {
                        var v = r.Volume.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string volume = v[0];
                        var frRam = r.Frequency.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecMB = v[0];// для фриквенси матери
                        if (r.FormFactor == "DIMM" && r.Price < 2600 && int.Parse(volume) == 8 && r.Price > max && r.Type == chosencpu.MemType && int.Parse(frecram) <= int.Parse(freccpu) && int.Parse(frecram) <= int.Parse(frecMB)) max = r.Price;
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
                else if (budget > 50000 && budget < 100000)
                {
                    foreach (RAM r in rams)
                    {
                        var v = r.Volume.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string volume = v[0];
                        var frRam = r.Frequency.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
                        var v = r.Volume.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string volume = v[0];
                        var frRam = r.Frequency.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string frecram = v[0];//for фриквенси для рам 
                        var frCPU = chosencpu.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string freccpu = v[0];// для фриквенси проца
                        var frMB = chosenmb.MemFreq.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
                if (budget < 51000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RecPowSup = r[0];
                        var po = p.Power.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string power = po[0];
                        if (p.Price < 3000 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == chosenmb.MainPins && p.CPUPins == chosenmb.CPUPins)
                        {
                            bool sixplus2 = false;
                            bool sixplus2x2 = false;
                            bool six = false;
                            bool eight = false;
                            bool eightplus6 = false;
                            bool eightplus8 = false;
                            if (p.GPUPins == "6+2") sixplus2 = true;
                            else if (p.GPUPins == "2*(6+2)") sixplus2x2 = true;
                            if (chosengpu.AddPowerPin == "6") six = true;
                            else if (chosengpu.AddPowerPin == "8") eight = true;
                            else if (chosengpu.AddPowerPin == "8+6") eightplus6 = true;
                            else if (chosengpu.AddPowerPin == "8+8") eightplus8 = true;

                            if ((chosengpu.AddPowerPin == "0")) max = p.Price;
                            else if (sixplus2 && (six || eight)) max = p.Price;
                            else if (sixplus2x2 && (six || eight || eightplus6 || eightplus8)) max = p.Price;
                        }
                    }
                    foreach (PowerSupply p in pss)
                    {
                        if (p.Price == max)
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
                        var r = chosengpu.RecomPowSup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RecPowSup = r[0];
                        var po = p.Power.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string power = po[0];
                        if (p.Price < 4200 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == chosenmb.MainPins && p.CPUPins == chosenmb.CPUPins)
                        {
                            bool sixplus2 = false;
                            bool sixplus2x2 = false;
                            bool six = false;
                            bool eight = false;
                            bool eightplus6 = false;
                            bool eightplus8 = false;
                            if (p.GPUPins == "6+2") sixplus2 = true;
                            else if (p.GPUPins == "2*(6+2)") sixplus2x2 = true;
                            if (chosengpu.AddPowerPin == "6") six = true;
                            else if (chosengpu.AddPowerPin == "8") eight = true;
                            else if (chosengpu.AddPowerPin == "8+6") eightplus6 = true;
                            else if (chosengpu.AddPowerPin == "8+8") eightplus8 = true;

                            if ((chosengpu.AddPowerPin == "0")) max = p.Price;
                            else if (sixplus2 && (six || eight)) max = p.Price;
                            else if (sixplus2x2 && (six || eight || eightplus6 || eightplus8)) max = p.Price;
                        }
                    }
                    foreach (PowerSupply p in pss)
                    {
                        if (p.Price == max)
                        {
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 71000 && budget < 91000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RecPowSup = r[0];
                        var po = p.Power.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string power = po[0];
                        if (p.Price < 5200 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == chosenmb.MainPins && p.CPUPins == chosenmb.CPUPins)
                        {
                            bool sixplus2 = false;
                            bool sixplus2x2 = false;
                            bool six = false;
                            bool eight = false;
                            bool eightplus6 = false;
                            bool eightplus8 = false;
                            if (p.GPUPins == "6+2") sixplus2 = true;
                            else if (p.GPUPins == "2*(6+2)") sixplus2x2 = true;
                            if (chosengpu.AddPowerPin == "6") six = true;
                            else if (chosengpu.AddPowerPin == "8") eight = true;
                            else if (chosengpu.AddPowerPin == "8+6") eightplus6 = true;
                            else if (chosengpu.AddPowerPin == "8+8") eightplus8 = true;

                            if ((chosengpu.AddPowerPin == "0")) max = p.Price;
                            else if (sixplus2 && (six || eight)) max = p.Price;
                            else if (sixplus2x2 && (six || eight || eightplus6 || eightplus8)) max = p.Price;
                        }
                    }
                    foreach (PowerSupply p in pss)
                    {
                        if (p.Price == max)
                        {
                            sb.AppendFormat("Блок питания:" + p.Name + " " + p.Power + " ——— " + p.Price.ToString());
                            chosenps = p;
                        }
                    }
                }
                else if (budget >= 91000 && budget < 150000)
                {
                    foreach (PowerSupply p in pss)
                    {
                        var r = chosengpu.RecomPowSup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RecPowSup = r[0];
                        var po = p.Power.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string power = po[0];
                        if (p.Price < 6100 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == chosenmb.MainPins && p.CPUPins == chosenmb.CPUPins)
                        {
                            bool sixplus2 = false;
                            bool sixplus2x2 = false;
                            bool six = false;
                            bool eight = false;
                            bool eightplus6 = false;
                            bool eightplus8 = false;
                            if (p.GPUPins == "6+2") sixplus2 = true;
                            else if (p.GPUPins == "2*(6+2)") sixplus2x2 = true;
                            if (chosengpu.AddPowerPin == "6") six = true;
                            else if (chosengpu.AddPowerPin == "8") eight = true;
                            else if (chosengpu.AddPowerPin == "8+6") eightplus6 = true;
                            else if (chosengpu.AddPowerPin == "8+8") eightplus8 = true;

                            if ((chosengpu.AddPowerPin == "0")) max = p.Price;
                            else if (sixplus2 && (six || eight)) max = p.Price;
                            else if (sixplus2x2 && (six || eight || eightplus6 || eightplus8)) max = p.Price;
                        }
                    }
                    foreach (PowerSupply p in pss)
                    {
                        if (p.Price == max)
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
                        var r = chosengpu.RecomPowSup.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RecPowSup = r[0];
                        var po = p.Power.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string power = po[0];
                        if (p.Price < 10000 && int.Parse(power) >= int.Parse(RecPowSup) && p.MBPins == chosenmb.MainPins && p.CPUPins == chosenmb.CPUPins)
                        {
                            bool sixplus2 = false;
                            bool sixplus2x2 = false;
                            bool six = false;
                            bool eight = false;
                            bool eightplus6 = false;
                            bool eightplus8 = false;
                            if (p.GPUPins == "6+2") sixplus2 = true;
                            else if (p.GPUPins == "2*(6+2)") sixplus2x2 = true;
                            if (chosengpu.AddPowerPin == "6") six = true;
                            else if (chosengpu.AddPowerPin == "8") eight = true;
                            else if (chosengpu.AddPowerPin == "8+6") eightplus6 = true;
                            else if (chosengpu.AddPowerPin == "8+8") eightplus8 = true;

                            if ((chosengpu.AddPowerPin == "0")) max = p.Price;
                            else if (sixplus2 && (six || eight)) max = p.Price;
                            else if (sixplus2x2 && (six || eight || eightplus6 || eightplus8)) max = p.Price;
                        }
                    }
                    foreach (PowerSupply p in pss)
                    {
                        if (p.Price == max)
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
                        if (s.Volume == "240 ГБ" && s.Price < max) max = s.Price;
                    }
                    foreach (SSD s in ssds)
                    {
                        if (s.Price == max)
                        {
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + "гб" + " ——— " + s.Price.ToString());
                            sb.AppendLine();
                            chosenssd = s;
                        }
                    }
                }
                else if (budget >= 50000 && budget < 80000)
                {
                    foreach (SSD s in ssds)
                    {
                        if (s.Volume == "480 ГБ" && s.Price < max) max = s.Price;
                    }
                    foreach (SSD s in ssds)
                    {
                        if (s.Price == max)
                        {
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + "гб" + " ——— " + s.Price.ToString());
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
                        if (s.Volume == "1000 ГБ" && s.Price < max1 && s.Price > max) max = s.Price;
                    }
                    foreach (SSD s in ssds)
                    {
                        if (s.Price == max)
                        {
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + "гб" + " ——— " + s.Price.ToString());
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
                            sb.AppendFormat("Твердотельный накопитель:" + s.Name + " " + s.Volume + "гб" + " ——— " + s.Price.ToString());
                            sb.AppendLine();
                            chosenssd = s;
                        }
                    }
                }

                //Выбираем корпус
                max = budget - (chosencpu.Price + chosengpu.Price + chosenmb.Price + chosenps.Price + chosenram.Price + chosenram.Price + chosenssd.Price);
                int max0 = 0;
                var l = chosengpu.Length.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string length = l[0];
                foreach (Case c in cs)
                {
                    var le = c.MaxGPULength.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string lenCase = le[0];
                    if (c.Price < max && c.Price > max0 && int.Parse(length) <= int.Parse(lenCase)) max0 = c.Price;
                }
                foreach (Case c in cs)
                {
                    if (c.Price == max0)
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
