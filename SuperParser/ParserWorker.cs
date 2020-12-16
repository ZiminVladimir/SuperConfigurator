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
            for (int i = parserSettings.StartPoint; i <= 1; i++)
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
                        PricesGPU.AddRange(parser.ParsePrice(document));
                        NamesGPU.AddRange(parser.ParseName(document));
                            list.AddRange(resultGPU);
                            Worker1(list, j, i);
                        }
                    else if (j == 1)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultMB = parser.Parse(document);
                        PricesMB.AddRange(parser.ParsePrice(document));
                        NamesMB.AddRange(parser.ParseName(document));
                            list.AddRange(resultMB);
                            Worker1(list, j, i);
                        }
                    else if (j == 2)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCPU = parser.Parse(document);
                        PricesCPU.AddRange(parser.ParsePrice(document));
                        NamesCPU.AddRange(parser.ParseName(document));
                            list.AddRange(resultCPU);
                            Worker1(list, j, i);
                        }
                    else if (j == 3)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultRAM = parser.Parse(document);
                        PricesRAM.AddRange(parser.ParsePrice(document));
                        NamesRAM.AddRange(parser.ParseName(document));
                            list.AddRange(resultRAM);
                            Worker1(list, j, i);
                        }
                    else if (j == 4)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultSSD = parser.Parse(document);
                        PricesSSD.AddRange(parser.ParsePrice(document));
                        NamesSSD.AddRange(parser.ParseName(document));
                            list.AddRange(resultSSD);
                        Worker1(list, j, i);
                    }
                    else if (j == 5)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultCAS = parser.Parse(document);
                        PricesCAS.AddRange(parser.ParsePrice(document));
                        NamesCAS.AddRange(parser.ParseName(document));
                            list.AddRange(resultCAS);
                            Worker1(list, j, i);
                        }
                    else if (j == 6)
                    {
                            HtmlParser domParser = new HtmlParser();
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            resultPS = parser.Parse(document);
                        PricesPS.AddRange(parser.ParsePrice(document));
                        NamesPS.AddRange(parser.ParseName(document));
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
        public async void Worker1(List<string> list,int j, int PageCount)
        {
            List<List<string>> res = new List<List<string>>();
            List<string> result1 = new List<string>();
            List<string> categoties = new List<string>();
            List<string> hrefs = new List<string>();
            //ContainerGPU CGPU = new ContainerGPU();
            int count = 0 + PageCount * 24;
            for (int i = count; i < list.Count; i++)
            {
                string source = await GetSourceByPage1(i, list);
                HtmlParser domParser = new HtmlParser();
                IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                if (j == 0)
                {
                    result1 = parser.Parse1(document);
                    GPU_Add(result1, i);
                }
                else if (j == 1)
                {
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    MotherBoard_Add(result1, i);
                }
                else if (j == 2)
                {
                    result1 = parser.Parse1(document);
                    CPU_Add(result1, i);
                }
                else if (j == 3)
                {
                    result1 = parser.ParseRAM(document);
                    RAM_Add(result1, i);
                }
                else if (j == 4)
                {
                    result1 = parser.ParseRAM(document);
                    SSD_Add(result1, i);
                }
                else if (j == 5)
                {
                    hrefs = parser.ParseMB(document);
                    string source1 = await GetSourceByPageMB(i, list, hrefs);
                    HtmlParser domParser1 = new HtmlParser();
                    IHtmlDocument document1 = await domParser1.ParseDocumentAsync(source1);
                    result1 = parser.Parse1(document1);
                    Case_Add(result1, i);
                }
                else if (j == 6)
                {
                    result1 = parser.Parse1(document);
                    categoties = parser.ParseCategories(document);
                    PowerSupply_Add(result1, i, categoties);
                }
            }


            OnComplited?.Invoke(this);
            isActive = false;
            if (CCPU.listCPU.Count == 48 && CGPU.listGPU.Count == 48 && CPS.listPS.Count == 48 && CMB.listMB.Count == 48 && CSSD.listSSD.Count == 48 && CCase.listCase.Count == 48 && CRAM.listRAM.Count == 48)
            {
                OnComplited?.Invoke(this);
            }

        }

        public void RAM_Add(List<string> list, int j)
        {
            int price;
            string Name = NamesRAM[j];
            string Type = "";
            string Frequency = "";
            string Volume = "";
            string FormFactor = "";
            string Number = "";
            var prices1 = PricesRAM[j].Split();
            price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);

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
                else if(i.Contains("DIMM"))
                {
                    FormFactor = i;
                }
                else if(i.Contains("шт"))
                {
                    Number = i;
                }

            }
            RAM ram = new RAM(price, Name, Type, Frequency, Volume, FormFactor, Number);
            CRAM.Add(ram);
        }
        public void PowerSupply_Add(List<string> list, int j, List<string>categories)
        {
            int price;
            string name = NamesPS[j];
            string power = list[0];
            string GPUPins6 = "";
            string GPUPins8 = "";
            string MBPins = "";
            string CPUPins = "";
            var prices1 = PricesPS[j].Split();
            if (prices1.Length > 15)
            {
                price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);



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

        public void CPU_Add(List<string> list, int j)
        {
            int price;
            string name = NamesCPU[j];
            string socket = "";
            string cores = "";
            string threads = "";
            string chipset = "";
            string memvol = "";
            string memfreq = "";
            string memtype = "";
            var prices1 = PricesCPU[j].Split();
            price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);

            foreach (string i in list)
            {
                if (i.Contains("AMD")&&socket=="")
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
        
        public void GPU_Add(List<string> list, int j)
        {
            // гитхаб сосать
            int price = 0;
            string name = NamesGPU[j];
            string mem = null;
            string addpower = null;
            string recbp = null;
            string power = null;
            string length = null;
            var prices1 = PricesGPU[j].Split();
            //var prices2 = prices1[0].Split();
            price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);
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
        public void MotherBoard_Add(List<string> list, int j)
        {
            // гитхаб сосать
            int price = 0;
            string name = NamesMB[j];
            string socket = list[1];
            string Chipset = null;
            string MemType = null;
            string MemFreq = null;
            string MemVol=null;
            bool M_2=true;
            string MainPins=null;
            string CPUPins=null;
            var prices1 = PricesMB[j].Split();
            price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);
            foreach (string i in list)
            {
                if(i.Contains("Intel"))
                {
                    Chipset = "Intel";
                }
                else if(i.Contains("AMD"))
                {
                    Chipset = "AMD";
                }
                else if(i.Contains("МГц"))
                {
                    MemFreq = i;
                }
                else if(i.Contains("ГБ"))
                {
                    MemVol = i;
                }
                else if(i.Contains("-контактный"))
                {
                    MainPins = i;
                }
                else if(i.Contains("-контактное"))
                {
                    CPUPins = i;
                }
            }
                MotherBoard mb = new MotherBoard(price, name, socket, Chipset, MemType, MemFreq, MemVol, M_2, MainPins, CPUPins);
            CMB.Add(mb);
            
        }
        public void Case_Add(List<string> list, int j)
        {
            int Price=0;
            string Name= NamesCAS[j];
            string MaxGPULength=null;
            var prices1 = PricesCAS[j].Split();
            Price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);
            foreach (string i in list)
            {
                if(i.Contains("мм")&&!i.Contains("x") &&MaxGPULength==null)
                {
                    MaxGPULength = i;
                }

            }
            Case cas = new Case(Price,Name,MaxGPULength);
            CCase.Add(cas);
        }
        public void HDD_Add(List<string> list, int j)
        {
              int Price=0;
              string Name= NamesHDD[j];
              string Volume=null;
              string Revs=null;
              var prices1 = PricesHDD[j].Split();
              Price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);
            foreach (string i in list)
            {
                if(i.Contains("ГБ"))
                {
                    Volume = i;
                }
                if(i.Contains("об/мин"))
                {
                    Revs = i;
                }
            }
            HDD hdd = new HDD(Price, Name, Volume, Revs);
            CHDD.Add(hdd);

        }
        public void SSD_Add(List<string> list, int j)
        {
             int Price=0;
             string Name= NamesSSD[j];
             string Volume=null;
             bool M_2=true;
            var prices1 = PricesSSD[j].Split();
            Price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);
            foreach (string i in list)
            {
                if(i.Contains("ГБ"))
                {
                    Volume = i;
                }    
            }
            SSD ssd = new SSD(Price, Name, Volume, M_2);
            CSSD.Add(ssd);
        }
        }
}
