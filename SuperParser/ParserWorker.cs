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
        List<string> Names = new List<string>();
        IParser parser;
        IParserSettings parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        bool isActive; //активность парсера
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
            isActive = true;
            // if (flag) Worker1();
            Worker();
            isActive = true;
        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }

        public async void Worker()
        {
            //ssilki[0] = "https://www.e-katalog.ru/MSI-GEFORCE-RTX-3070-SUPRIM-X-8G.htm";
            //for(int j =1;j<ssilki.Count;j++)
           // { 
            List<string> list = new List<string>();
            List<string> result = new List<string>();
            //string j = "https://www.e-katalog.ru/list/188/";
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                //if (IsActive)
                {
                    string source = await loader.GetSourceByPage(i, ssilki[6]); //Получаем код страницы
                                                                        //Здесь магия AngleShap, подробнее об интерфейсе IHtmlDocument и классе HtmlParser, 
                                                                        //можно прочитать на GitHub, это интересное чтиво с примерами.
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    result = parser.Parse(document);
                    Prices = parser.ParsePrice(document);
                    Names = parser.ParseName(document);
                    //OnNewData?.Invoke(this, result);
                }
            }


            isActive = false;
            flag = true;
            list.AddRange(result.ToArray());
            Worker1(list,6);
            count++;
            //}
            OnComplited?.Invoke(this);
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
        public async void Worker1(List<string> list,int j)
        {
            List<List<string>> res = new List<List<string>>();
            List<string> result1 = new List<string>();
            List<string> categoties = new List<string>();
            List<string> hrefs = new List<string>();
            //ContainerGPU CGPU = new ContainerGPU();
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string source = await GetSourceByPage1(i, list);
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    if(j==0)
                    {
                        result1 = parser.Parse1(document);
                        GPU_Add(result1, i);
                    }
                    else if (j == 1)
                    {
                        hrefs = parser.ParseMB(document);
                        string source1 = await GetSourceByPageMB(i, list,hrefs);
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
                        PowerSupply_Add(result1, i,categoties);
                    }
                }

                //OnComplited?.Invoke(this);
                isActive = false;
            }
        }

        public void RAM_Add(List<string> list, int j)
        {
            int price;
            string Name = Names[j];
            string Type = "";
            string Frequency = "";
            string Volume = "";
            string FormFactor = "";
            string Number = "";
            var prices1 = Prices[j].Split();
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
            string name = Names[j];
            string power = list[0];
            string GPUPins = "";
            string MBPins = "";
            string CPUPins = "";
            var prices1 = Prices[j].Split();
            price = Convert.ToInt32(prices1[2]) * 1000 + Convert.ToInt32(prices1[3]);

            foreach (string i in list)
            {
                if (i.Contains("pin"))
                {
                    MBPins = i[0].ToString() + i[1].ToString();
                    CPUPins = i[2].ToString();
                }
            }
            for(int i=0;i<categories.Count;i++)
            {
                if (categories[i].Contains("PCI-E"))
                {
                    var a = categories[i].Split();
                    GPUPins = a[1];
                }
            }
            PowerSupply ps = new PowerSupply(price, name, power, GPUPins, MBPins, CPUPins);
            CPS.Add(ps);
        }

        public void CPU_Add(List<string> list, int j)
        {
            int price;
            string name = Names[j];
            string socket = "";
            string cores = "";
            string threads = "";
            string chipset = "";
            string memvol = "";
            string memfreq = "";
            string memtype = "";
            var prices1 = Prices[j].Split();
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
            string name = Names[j];
            string mem = null;
            string addpower = null;
            string recbp = null;
            string power = null;
            string length = null;
            var prices1 = Prices[j].Split();
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
                    string number = i[0].ToString() + i[1].ToString() + i[2].ToString();
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
            string name = Names[j];
            string socket = list[1];
            string Chipset = null;
            string MemType = null;
            string MemFreq = null;
            string MemVol=null;
            bool M_2=true;
            string MainPins=null;
            string CPUPins=null;
            var prices1 = Prices[j].Split();
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
            string Name= Names[j];
            string MaxGPULength=null;
            var prices1 = Prices[j].Split();
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
              string Name= Names[j];
              string Volume=null;
              string Revs=null;
              var prices1 = Prices[j].Split();
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
             string Name= Names[j];
             string Volume=null;
             bool M_2=true;
            var prices1 = Prices[j].Split();
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
