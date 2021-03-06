﻿using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using PC_Components;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SuperParser
{
    public class ParserWorker
    {
        public bool flag;
        readonly HttpClient client = new HttpClient();
        List<string> ssilki = new List<string>() 
        { 
            "https://www.e-katalog.ru/list/189/",
            "https://www.e-katalog.ru/list/187/",
            "https://www.e-katalog.ru/list/186/",
            "https://www.e-katalog.ru/list/188/",
            "https://www.e-katalog.ru/list/61/",
            "https://www.e-katalog.ru/list/193/",
            "https://www.e-katalog.ru/list/351/" 
        };

        E_CatalogParser FP = new E_CatalogParser();
        List<string> Prices = new List<string>();
        public int count = 0;
        List<string> resultGPU = new List<string>();
        List<string> resultCPU = new List<string>();
        List<string> resultMB = new List<string>();
        List<string> resultRAM = new List<string>();
        List<string> resultPS = new List<string>();
        List<string> resultCAS = new List<string>();
        List<string> resultSSD = new List<string>();
        List<string> resultHDD = new List<string>();
        IParser parser;
        IParserSettings parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        ContainerPS CPS = new ContainerPS();
        ContainerCPU CCPU = new ContainerCPU();
        ContainerGPU CGPU = new ContainerGPU();
        ContainerMB CMB = new ContainerMB();
        ContainerCase CCase = new ContainerCase();
        ContainerHDD CHDD = new ContainerHDD();
        ContainerSSD CSSD = new ContainerSSD();
        ContainerRAM CRAM = new ContainerRAM();
        public ParserWorker() { }
        public IParser Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value; //Новые настройки парсера
                loader = new HtmlLoader(value); //сюда помещаются настройки для загрузчика кода страницы
            }
        }

        public ParserWorker(IParser parser)
        {
            this.parser = parser;
        }

        public void Start()
        {
                Worker();
        }


        public async void Worker()
        {
            for (int j = 0; j < ssilki.Count; j++)
            { 
            List<string> list = new List<string>();
            List<string> result = new List<string>();
            for (int i = 0; i <= 34; i++)
            {
                    string source = await loader.GetSourceByPage(i, ssilki[j]);
                    if (j == 0)
                    {
                            if (i > 36)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultGPU = parser.Parse(document);
                            list=resultGPU;
                            Worker1(list, j, i);
                        }
                    else if (j == 1)
                    {
                            if (i > 35)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultMB = parser.Parse(document);
                            list.AddRange(resultMB);
                            Worker1(list, j, i);
                        }
                    else if (j == 2)
                    {
                            if (i > 5)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCPU = parser.Parse(document);
                            list=resultCPU;
                            Worker1(list, j, i);
                        }
                    else if (j == 3)
                    {
                            if (i > 21)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultRAM = parser.Parse(document);
                            list.AddRange(resultRAM);
                            Worker1(list, j, i);
                        }
                    else if (j == 4)
                    {
                            if (i > 21)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultSSD = parser.Parse(document);
                            list.AddRange(resultSSD);
                        Worker1(list, j, i);
                    }
                    else if (j == 5)
                    {
                            if (i > 66)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCAS = parser.Parse(document);
                            list.AddRange(resultCAS);
                            Worker1(list, j, i);
                        }
                    else if (j == 6)
                    {
                            if (i > 11)
                            {
                                continue;
                            }
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultPS = parser.Parse(document);
                            list.AddRange(resultPS);
                            Worker1(list, j, i);
                    }
                
            }

            }            
        }

        public async Task<string> GetSourceByPage1(int i, List<string> list)
        {
            string mainurl = "https://www.e-katalog.ru/";
            string currentUrl1 = mainurl + list[i];
            HttpResponseMessage responce = await client.GetAsync(currentUrl1); //Получаем ответ с сайта
            string source = default;
            source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную
            return source;
        }

        public async Task<string> GetSourceByPageMB(int i, List<string> list, List<string>href)
        {
            string mainurl = href[0];
            HttpResponseMessage responce = await client.GetAsync(mainurl); //Получаем ответ с сайта
            string source = default;
            source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную
            
            return source;
        }
        public object locker = new object();
        public async void Worker1(List<string> list,int k, int PageCount)
        {
            List<List<string>> res = new List<List<string>>();
            List<string> result1 = new List<string>();
            List<string> categoties = new List<string>();
            List<string> hrefs = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                string source = await GetSourceByPage1(i, list);
                
                if (k == 0)
                {
                    List<string> PricesGPU1 = new List<string>();
                    List<string> NamesGPU1 = new List<string>();
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesGPU1 = parser.ParsePrice(document);
                    if(PricesGPU1.Count==0)
                    {
                        continue;
                    }
                    NamesGPU1 = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    GPU_Add(result1, i, PricesGPU1,NamesGPU1);
                }
                else if (k == 1)
                {
                    List<string> PricesMB1 = new List<string>();
                    List<string> NamesMB1 = new List<string>();
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesMB1 = parser.ParsePrice(document);
                    if (PricesMB1.Count == 0)
                    {
                        continue;
                    }
                    
                    NamesMB1 = parser.ParseNameBlue(document);
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    categoties = parser.ParseCategories(document1);
                    MotherBoard_Add(result1, i, categoties,PricesMB1,NamesMB1);
                }
                else if (k == 2)
                {
                    List<string> PricesCPU1 = new List<string>();
                    List<string> NamesCPU1 = new List<string>();
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesCPU1 = parser.ParsePrice(document);
                    if (PricesCPU1.Count == 0)
                    {
                        continue;
                    }
                    NamesCPU1 = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    categoties = parser.ParseCategories(document);
                    CPU_Add(result1, i,categoties, PricesCPU1, NamesCPU1);
                }
                else if (k == 3)
                {
                    List<string> PricesRAM1 = new List<string>();
                    List<string> NamesRAM1 = new List<string>();
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesRAM1 = parser.ParsePrice(document);
                    if (PricesRAM1.Count == 0)
                    {
                        continue;
                    }
                    NamesRAM1 = parser.ParseName(document);
                    result1 = parser.ParseRAM(document);
                    RAM_Add(result1, i, PricesRAM1, NamesRAM1);
                }
                else if (k == 4)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    List<string> PricesSSD1 = new List<string>();
                    PricesSSD1 = parser.ParsePrice(document);
                    if (PricesSSD1.Count == 0)
                    {
                        continue;
                    }
                    List<string> NamesSSD1 = new List<string>();
                    NamesSSD1 = parser.ParseName(document);
                    result1 = parser.ParseRAM(document);
                    SSD_Add(result1, i, NamesSSD1, PricesSSD1);
                }
                else if (k == 5)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    List<string> PricesCAS1 = new List<string>();
                    PricesCAS1 = parser.ParsePrice(document);
                    if (PricesCAS1.Count == 0)
                    {
                        continue;
                    }
                    
                    List<string> NamesCAS1 = new List<string>();

                    NamesCAS1 = parser.ParseNameBlue(document);
                    if(NamesCAS1.Count==0)
                    {
                        continue;
                    }
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    Case_Add(result1, i, NamesCAS1, PricesCAS1);
                }
                else if (k == 6)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    List<string> PricesPS1 = new List<string>();
                    PricesPS1 = parser.ParsePrice(document);
                    if (PricesPS1.Count == 0)
                    {
                        continue;
                    }
                    List<string> NamesPS1 = new List<string>(); 
                    NamesPS1 = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    categoties = parser.ParseCategories(document);
                    PowerSupply_Add(result1, i, categoties, NamesPS1, PricesPS1);
                }
            }
            count++;
            if(count>=160)
            {
                Serialize();
            }

        }
        public void Serialize()
        {
            using(var sw = new StreamWriter("GPU.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CGPU.listGPU));
            }
            using (var sw = new StreamWriter("MB.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CMB.listMB));
            }
            using (var sw = new StreamWriter("CPU.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CCPU.listCPU));
            }
            using (var sw = new StreamWriter("RAM.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CRAM.listRAM));
            }
            using (var sw = new StreamWriter("Case.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CCase.listCase));
            }
            using (var sw = new StreamWriter("PS.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CPS.listPS));
            }
            using (var sw = new StreamWriter("SSD.json"))
            {
                sw.WriteLine(JsonConvert.SerializeObject(CSSD.listSSD));
            }
        }

        public void RAM_Add(List<string> list, int j,List<string>PricesRAM1,List<string>NamesRAM1)
        {
            int price;
            string Name = NamesRAM1[0];
            bool fl = true;
            if (list.Count == 0) fl = false;
            foreach (var r in CRAM.listRAM)
            {
                if (r.Name == Name) fl = false;
            }
            if (fl)
            {
                string Type = "";
                string Frequency = "";
                string Volume = "";
                string FormFactor = "";
                string Number = "";
                if (PricesRAM1.Count != 0)
                {
                    var prices1 = PricesRAM1[0].Split();
                    if (prices1.Length > 7)
                    {
                        price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);

                        foreach (string i in list)
                        {
                            if (i.Contains("DDR"))
                            {
                                Type = i;
                            }
                            else if (i.Contains("МГц"))
                            {
                                Frequency = i;
                            }
                            else if (i.Contains("ГБ"))
                            {
                                Volume = i;
                            }
                            else if (i.Contains("DIMM"))
                            {
                                FormFactor = i;
                            }
                            else if (i.Contains("шт"))
                            {
                                Number = i;
                            }

                        }
                        RAM ram = new RAM(price, Name, Type, Frequency, Volume, FormFactor, Number);
                        CRAM.Add(ram);
                    }
                }
            }
        }
        public void PowerSupply_Add(List<string> list, int j, List<string>categories, List<string> NamesPS1, List<string> PricesPS1)
        {
            int price;
            string name = NamesPS1[0];
            if (list.Count != 0)
            {
                bool fl = true;
                foreach (var p in CPS.listPS)
                {
                    if (p.Name == name) fl = false;
                }
                if (fl)
                {
                    string power = list[0];
                    string GPUPins6 = "";
                    string GPUPins8 = "";
                    string MBPins = "";
                    string CPUPins = "";
                    if (PricesPS1.Count != 0)
                    {
                        var prices1 = PricesPS1[0].Split();
                        if (prices1.Length > 7)
                        {
                            price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                            foreach (string i in list)
                            {
                                if (i.Contains("pin") && MBPins == "")
                                {
                                    MBPins = i[0].ToString() + i[1].ToString();
                                    CPUPins = i[3].ToString();
                                }
                            }
                            for (int i = 0; i < categories.Count; i++)
                            {
                                if (categories[i].Contains("6pin"))
                                {
                                    GPUPins6 = list[i];
                                }
                                else if (categories[i].Contains("8pin"))
                                {
                                    GPUPins8 = list[i];
                                }
                            }
                            PowerSupply ps = new PowerSupply(price, name, power, GPUPins6, GPUPins8, MBPins, CPUPins);
                            CPS.Add(ps);
                        }
                    }
                }
            }
        }

        public void CPU_Add(List<string> list, int j,List<string>categories, List<string> PricesCPU1, List<string> NamesCPU1)
        {
            int price;
            string name = NamesCPU1[0];
            if (name != "AMD 2650")
            {
                bool fl = true;
                if (list.Count == 0) fl = false;
                
                if (fl)
                {
                    string socket = "";

                    string cores = "";
                    string threads = "";
                    string chipset = "";
                    string memvol = "";
                    string memfreq = "";
                    string memtype = "";
                    if (PricesCPU1.Count != 0)
                    {
                        var prices1 = PricesCPU1[0].Split();
                        price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                        foreach (string i in categories)
                        {
                            if (i.Contains("DDR"))
                            {
                                var ddr = i.Split();
                                memtype = ddr[ddr.Length - 1];
                            }
                        }
                        foreach (string i in list)
                        {
                            if (i.Contains("AMD") && socket == "")
                            {
                                chipset = "AMD";
                                var soc = i.Split();
                                socket = soc[1];
                            }
                            else if (i.Contains("Intel") && socket == "")
                            {
                                chipset = "Intel";
                                var soc = i.Split();
                                if (soc.Length == 2)
                                {
                                    socket = soc[0] + " " + soc[1];
                                }
                                else if (soc.Length == 3)
                                {
                                    socket = soc[0] + " " + soc[1] + " " + soc[2];
                                }
                                else if (soc.Length == 4)
                                {
                                    socket = soc[0] + " " + soc[1] + " " + soc[2] + " " + soc[3];
                                }
                                else socket = soc[1];
                            }
                            else if (i.Contains("cores"))
                            {
                                cores = i;
                            }
                            else if (i.Contains("threads"))
                            {
                                threads = i;
                            }
                            else if (i.Contains("ГБ"))
                            {
                                memvol = i;
                            }
                            else if (i.Contains("МГц"))
                            {
                                memfreq = i;
                            }
                        }
                        CPU Cpu = new CPU(price, name, socket, cores, threads, chipset, memvol, memfreq, memtype);
                        CCPU.Add(Cpu);
                    }
                }
            }
        }
        
        public void GPU_Add(List<string> list, int j, List<string> PricesGPU1,List<string>NamesGPU1)
        {
            int price = 0;
            string name = NamesGPU1[0];
            bool fl = true;
            if (list.Count == 0) fl = false;
            if (fl)
            {
                string mem = null;
                string addpower = null;
                string recbp = null;
                string power = null;
                string length = null;
                if (PricesGPU1.Count != 0)
                {
                    var prices1 = PricesGPU1[0].Split();
                    price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                    foreach (string i in list)
                    {
                        if (i.Contains("ГБ"))
                        {
                            mem = i;
                        }
                        else if (i.Contains("pin"))
                        {
                            addpower = i;
                        }
                        else if (i.Contains("Вт"))
                        {
                            var p = i.Split();
                            string number = p[0];
                            if (power == null && Convert.ToInt32(number) < 300)
                            {
                                power = i;
                            }
                            else
                                recbp = i;
                        }
                        else if (i.Contains("мм"))
                        {
                            length = i;
                        }
                    }
                    if (power == null)
                    {
                        power = "200 Вт";
                    }
                    GPU gpu = new GPU(price, name, mem, power, recbp, length, addpower);
                    CGPU.Add(gpu);
                }
            }
        }
        public void MotherBoard_Add(List<string> list, int j, List<string> categories, List<string> PricesMB1, List<string> NamesMB1)
        {
            int price = 0;
            bool fl = true; 
            string name = NamesMB1[0];
            if (list.Count != 0)
            {
                if (name == " ASRock B450 Steel Legend")
                {
                    fl = true;
                }
                if (name == " ASRock B550M Pro4") fl = false;
                foreach (var g in CMB.listMB)
                {
                    if (g.Name == name) fl = false;
                }
                if (fl)
                {
                    string socket = list[1];
                    string Chipset = null;
                    string MemType = null;
                    string MemFreq = null;
                    string MemVol = null;
                    bool M_2 = true;
                    string MainPins = null;
                    string CPUPins = null;
                    if (PricesMB1.Count != 0)
                    {
                        var prices1 = PricesMB1[0].Split();
                        price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                        foreach (string i in categories)
                        {
                            if (i.Contains("DDR"))
                            {
                                var ddr = i.Split();
                                MemType = ddr[ddr.Length - 1];
                            }
                        }
                        foreach (string i in list)
                        {
                            if (i.Contains("Intel"))
                            {
                                Chipset = "Intel";
                            }
                            else if (i.Contains("AMD"))
                            {
                                Chipset = "AMD";
                            }
                            else if (i.Contains("МГц"))
                            {
                                MemFreq = i;
                            }
                            else if (i.Contains("ГБ"))
                            {
                                MemVol = i;
                            }
                            else if (i.Contains("-контактный"))
                            {
                                MainPins = i;
                            }
                            else if (i.Contains("-контактное"))
                            {
                                CPUPins = i;
                            }
                        }
                        MotherBoard mb = new MotherBoard(price, name, socket, Chipset, MemType, MemFreq, MemVol, M_2, MainPins, CPUPins);
                        CMB.Add(mb);
                    }
                }
            }
            
        }
        public void Case_Add(List<string> list, int j, List<string> NamesCAS1, List<string> PricesCAS1)
        {
            int Price=0;
            string Name= NamesCAS1[0];
            bool fl = true;
            if (list.Count == 0) fl = false;
            foreach (var c in CCase.listCase)
            {
                if (c.Name == Name) fl = false;
            }
            if (fl)
            {
                string MaxGPULength = null;
                if (PricesCAS1.Count != 0)
                {
                    var prices1 = PricesCAS1[0].Split();
                    Price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                    foreach (string i in list)
                    {
                        if (i.Contains("мм") && !i.Contains("x") && MaxGPULength == null)
                        {
                            MaxGPULength = i;
                        }

                    }
                    Case cas = new Case(Price, Name, MaxGPULength);
                    CCase.Add(cas);
                }
            }
        }
        public void SSD_Add(List<string> list, int j, List<string> NamesSSD1, List<string> PricesSSD1)
        {
             int Price=0;
             string Name= NamesSSD1[0];
            bool fl = true;
            if (list.Count == 0) fl = false;
            foreach (var s in CSSD.listSSD)
            {
                if (s.Name == Name) fl = false;
            }
            if (fl)
            {
                string Volume = null;
                bool M_2 = true;
                if (PricesSSD1.Count != 0)
                {
                    var prices1 = PricesSSD1[0].Split();
                    Price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                    foreach (string i in list)
                    {
                        if (i.Contains("ГБ"))
                        {
                            Volume = i;
                        }
                    }
                    SSD ssd = new SSD(Price, Name, Volume, M_2);
                    CSSD.Add(ssd);
                }
            }
        }
    }
}
