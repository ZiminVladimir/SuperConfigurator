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
        IParser parser;
        IParserSettings parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        bool isActive; //активность парсера
        ContainerGPU CGPU = new ContainerGPU();
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
            //foreach (string j in ssilki)
            //{
            List<string> list = new List<string>();
            List<string> result = new List<string>();
            string j = "https://www.e-katalog.ru/list/189/";
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                //if (IsActive)
                {
                    string source = await loader.GetSourceByPage(i, j); //Получаем код страницы
                                                                        //Здесь магия AngleShap, подробнее об интерфейсе IHtmlDocument и классе HtmlParser, 
                                                                        //можно прочитать на GitHub, это интересное чтиво с примерами.
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    result = parser.Parse(document);
                    Prices = parser.ParsePrice(document);
                    //OnNewData?.Invoke(this, result);
                }
            }


            isActive = false;
            flag = true;
            list.AddRange(result.ToArray());
            Worker1(list);
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
        public async void Worker1(List<string> list)
        {
            List<List<string>> res = new List<List<string>>();
            List<string> result1 = new List<string>();
            //ContainerGPU CGPU = new ContainerGPU();
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string source = await GetSourceByPage1(i, list);
                    HtmlParser domParser = new HtmlParser();
                    IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                    result1 = parser.Parse1(document);
                    GPU_Add(result1, i);
                }

                //OnComplited?.Invoke(this);
                isActive = false;
            }
        }
        public void GPU_Add(List<string> list, int j)
        {
            // гитхаб сосать
            int price = 0;
            string name = list[1];
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
    }
}
