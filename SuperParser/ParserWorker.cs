using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using SuperParser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using PC_Components;
using System.Threading.Tasks;

namespace SuperParser
{
    public class ParserWorker
    {
        public bool flag;
        readonly HttpClient client = new HttpClient();
        // public List<string> list = new List<string>();
        List<string> ssilki = new List<string>() { "https://www.e-katalog.ru/list/189/",
            "https://www.e-katalog.ru/list/187/",
            "https://www.e-katalog.ru/list/186/",
            "https://www.e-katalog.ru/list/188/",
            "https://www.e-katalog.ru/list/61/",
            "https://www.e-katalog.ru/list/193/",
            "https://www.e-katalog.ru/list/351/" };
        E_CatalogParser FP = new E_CatalogParser();
        List<string> Prices = new List<string>();
        public int count = 0;
        List<string> PricesGPU = new List<string>();
        List<string> PricesCPU = new List<string>();
        List<string> PricesMB = new List<string>();
        List<string> PricesRAM = new List<string>();
        List<string> PricesPS = new List<string>();
        List<string> PricesCAS = new List<string>();
        List<string> PricesSSD = new List<string>();
        List<string> PricesHDD = new List<string>();
        List<string> NamesGPU = new List<string>();
        List<string> NamesCPU = new List<string>();
        List<string> NamesMB = new List<string>();
        List<string> NamesRAM = new List<string>();
        List<string> NamesPS = new List<string>();
        List<string> NamesCAS = new List<string>();
        List<string> NamesSSD = new List<string>();
        List<string> NamesHDD = new List<string>();
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
        bool isActive=true; //активность парсера
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

        public bool IsActive //проверяем активность парсера.
        {
            get { return isActive; }
        }

        //Это событие возвращает спаршенные за итерацию данные( первый аргумент ссылка на парсер, и сами данные вторым аргументом)
        //public event Action<object, T> OnNewData;
        //Это событие отвечает информирование при завершении работы парсера.
        public event Action<object> OnComplited;

        //1-й конструктор, в качестве аргумента будет передеваться класс реализующий интерфейс IParser
        public ParserWorker(IParser parser)
        {
            this.parser = parser;
        }

        public void Start() //Запускаем парсер
        {
            // if (flag) Worker1();
                Worker();
        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }

        public async void Worker()
        {
            //ssilki[0] = "https://www.e-katalog.ru/MSI-GEFORCE-RTX-3070-SUPRIM-X-8G.htm";
            for (int j = 0; j < ssilki.Count; j++)
            { 
            List<string> list = new List<string>();
            List<string> result = new List<string>();
            //string j = "https://www.e-katalog.ru/list/188/";
            for (int i = 0; i <= 4; i++)
            {
                //if (IsActive)
                {
                    string source = await loader.GetSourceByPage(i, ssilki[j]); //Получаем код страницы
                                                                        //Здесь магия AngleShap, подробнее об интерфейсе IHtmlDocument и классе HtmlParser, 
                                                                        //можно прочитать на GitHub, это интересное чтиво с примерами.
                    if (j == 0)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultGPU = parser.Parse(document);
                            list=resultGPU;
                            Worker1(list, j, i);
                        }
                    else if (j == 1)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultMB = parser.Parse(document);
                            list.AddRange(resultMB);
                            Worker1(list, j, i);
                        }
                    else if (j == 2)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCPU = parser.Parse(document);
                            list=resultCPU;
                            Worker1(list, j, i);
                        }
                    else if (j == 3)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultRAM = parser.Parse(document);
                            list.AddRange(resultRAM);
                            Worker1(list, j, i);
                        }
                    else if (j == 4)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultSSD = parser.Parse(document);
                            list.AddRange(resultSSD);
                        Worker1(list, j, i);
                    }
                    else if (j == 5)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCAS = parser.Parse(document);
                            list.AddRange(resultCAS);
                            Worker1(list, j, i);
                        }
                    else if (j == 6)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultPS = parser.Parse(document);
                            list.AddRange(resultPS);
                            Worker1(list, j, i);
                        }
                }
                    //OnNewData?.Invoke(this, result);
            }

            }            
            Console.WriteLine("1");
        }

        public async Task<string> GetSourceByPage1(int i, List<string> list) // id - это id страницы
        {
            string mainurl = "https://www.e-katalog.ru/";
            string currentUrl1 = mainurl + list[i];//Подменяем {CurrentId} на номер страницы
            HttpResponseMessage responce = await client.GetAsync(currentUrl1); //Получаем ответ с сайта.
            string source = default;
            //if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
        public async Task<string> GetSourceByPageMB(int i, List<string> list,List<string>href) // id - это id страницы
        {
            string mainurl = href[0];
            //string currentUrl1 = mainurl + list[i];//Подменяем {CurrentId} на номер страницы
            HttpResponseMessage responce = await client.GetAsync(mainurl); //Получаем ответ с сайта.
            string source = default;
            //if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
        public object locker = new object();
        public async void Worker1(List<string> list,int j, int PageCount)
        {
            List<List<string>> res = new List<List<string>>();
            List<string> result1 = new List<string>();
            List<string> categoties = new List<string>();
            List<string> hrefs = new List<string>();
            //ContainerGPU CGPU = new ContainerGPU();
            int count = 0 + PageCount * 24;

            for (int i = 0; i < list.Count; i++)
            {
                if (i==0)
                {
                    i++;
                    i--;
                }
                string source = await GetSourceByPage1(i, list);
                
                if (j == 0)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesGPU = parser.ParsePrice(document);
                    if(PricesGPU.Count==0)
                    {
                        continue;
                    }
                    NamesGPU = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    GPU_Add(result1, i);
                }
                else if (j == 1)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesMB = parser.ParsePrice(document);
                    if (PricesMB.Count == 0)
                    {
                        continue;
                    }
                    NamesMB = parser.ParseNameBlue(document);
                    //if (NamesMB[0] == " ASRock B550M Pro4")
                    //{

                    //}
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    MotherBoard_Add(result1, i);
                }
                else if (j == 2)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesCPU = parser.ParsePrice(document);
                    if (PricesCPU.Count == 0)
                    {
                        continue;
                    }
                    NamesCPU = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    CPU_Add(result1, i);
                }
                else if (j == 3)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesRAM = parser.ParsePrice(document);
                    if (PricesRAM.Count == 0)
                    {
                        continue;
                    }
                    NamesRAM = parser.ParseName(document);
                    result1 = parser.ParseRAM(document);
                    RAM_Add(result1, i);
                }
                else if (j == 4)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesSSD = parser.ParsePrice(document);
                    if (PricesSSD.Count == 0)
                    {
                        continue;
                    }
                    NamesSSD = parser.ParseName(document);
                    result1 = parser.ParseRAM(document);
                    SSD_Add(result1, i);
                }
                else if (j == 5)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesCAS = parser.ParsePrice(document);
                    if (PricesCAS.Count == 0)
                    {
                        continue;
                    }
                    NamesCAS = parser.ParseNameBlue(document);
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    Case_Add(result1, i);
                }
                else if (j == 6)
                {
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    PricesPS = parser.ParsePrice(document);
                    if (PricesPS.Count == 0)
                    {
                        continue;
                    }
                    NamesPS = parser.ParseName(document);
                    result1 = parser.Parse1(document);
                    categoties = parser.ParseCategories(document);
                    PowerSupply_Add(result1, i, categoties);
                }
            }

            //OnComplited?.Invoke(this);
            isActive = false;
            if (CCPU.listCPU.Count >= 91 && CGPU.listGPU.Count >= 113 && CPS.listPS.Count >= 84 && CMB.listMB.Count >= 117 && CSSD.listSSD.Count >= 119 && CCase.listCase.Count >= 111 && CRAM.listRAM.Count >= 93)
            {
                OnComplited?.Invoke(this);
            }

        }

        public void RAM_Add(List<string> list, int j)
        {
            int price;
            string Name = NamesRAM[0];
            bool fl = true;
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
                if (PricesRAM.Count != 0)
                {
                    var prices1 = PricesRAM[0].Split();
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
        public void PowerSupply_Add(List<string> list, int j, List<string>categories)
        {
            int price;
            string name = NamesPS[0];
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
                if (PricesPS.Count != 0)
                {
                    var prices1 = PricesPS[0].Split();
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

        public void CPU_Add(List<string> list, int j)
        {
            int price;
            string name = NamesCPU[0];
            bool fl = true;
            //foreach (var c in CCPU.listCPU)
            {
                //if (c.Name == name) fl = false;
            }
            if (fl)
            {
                string socket = "";
                string cores = "";
                string threads = "";
                string chipset = "";
                string memvol = "";
                string memfreq = "";
                string memtype = "";
                if (PricesCPU.Count != 0)
                {
                    var prices1 = PricesCPU[0].Split();
                    price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);

                    foreach (string i in list)
                    {
                        if (i.Contains("AMD") && socket == "")
                        {
                            chipset = "AMD";
                            var soc = i.Split();
                            socket = soc[1];
                        }
                        else if (i.Contains("Intel"))
                        {
                            chipset = "Intel";
                            var soc = i.Split();
                            if (soc.Length > 2) socket = soc[1] + " " + soc[2];
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
        
        public void GPU_Add(List<string> list, int j)
        {
            // гитхаб сосать
            int price = 0;
            string name = NamesGPU[0];
            bool fl = true;
            //foreach (var g in CGPU.listGPU)
            {
                //if (g.Name == name) fl = false;
            }
            if (fl)
            {
                string mem = null;
                string addpower = null;
                string recbp = null;
                string power = null;
                string length = null;
                if (PricesGPU.Count != 0)
                {
                    var prices1 = PricesGPU[0].Split();
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
        public void MotherBoard_Add(List<string> list, int j)
        {
            // гитхаб сосать
            int price = 0;
            bool fl = true; ;
            string name = NamesMB[0];
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
                if (PricesMB.Count != 0)
                {
                    var prices1 = PricesMB[0].Split();
                    price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
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
        public void Case_Add(List<string> list, int j)
        {
            int Price=0;
            string Name= NamesCAS[0];
            bool fl = true;
            foreach (var c in CCase.listCase)
            {
                if (c.Name == Name) fl = false;
            }
            if (fl)
            {
                string MaxGPULength = null;
                if (PricesCAS.Count != 0)
                {
                    var prices1 = PricesCAS[0].Split();
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
        public void HDD_Add(List<string> list, int j)
        {
              int Price=0;
              string Name= NamesHDD[0];
            bool fl = true;
            foreach (var h in CHDD.listHDD)
            {
                if (h.Name == Name) fl = false;
            }
            if (fl)
            {
                string Volume = null;
                string Revs = null;
                if (PricesHDD.Count != 0)
                {
                    var prices1 = PricesHDD[0].Split();
                    Price = Convert.ToInt32(prices1[1]) * 1000 + Convert.ToInt32(prices1[2]);
                    foreach (string i in list)
                    {
                        if (i.Contains("ГБ"))
                        {
                            Volume = i;
                        }
                        if (i.Contains("об/мин"))
                        {
                            Revs = i;
                        }
                    }
                    HDD hdd = new HDD(Price, Name, Volume, Revs);
                    CHDD.Add(hdd);
                }
            }

        }
        public void SSD_Add(List<string> list, int j)
        {
             int Price=0;
             string Name= NamesSSD[0];
            bool fl = true;
            foreach (var s in CSSD.listSSD)
            {
                if (s.Name == Name) fl = false;
            }
            if (fl)
            {
                string Volume = null;
                bool M_2 = true;
                if (PricesSSD.Count != 0)
                {
                    var prices1 = PricesSSD[0].Split();
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
