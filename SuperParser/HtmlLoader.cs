using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using SuperParser;
using System.Collections.Generic;

namespace SuperParser
{
    class HtmlLoader
    {
        readonly HttpClient client; //для отправки HTTP запросов и получения HTTP ответов.
        readonly string url; //сюда будем передовать адрес.
        public ParserWorker pw = new ParserWorker();
        List<string> list = new List<string>();
        //string mainurl = "https://www.e-katalog.ru/";
        //string currentUrl1;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Это для индентификации на сайте-жертве.
            url = $"{settings.BaseUrl}/{settings.Postfix}/"; //Здесь собирается адресная строка
        }

        public async Task<string> GetSourceByPage(int id, string i) // id - это id страницы
        {
            string currentUrl = url.Replace("https://www.e-katalog.ru/list/189/", i);
            currentUrl = url.Replace("{CurrentId}", id.ToString());//Подменяем {CurrentId} на номер страницы
            //string currentUrl = "https://www.e-katalog.ru/MSI-GEFORCE-RTX-3070-SUPRIM-X-8G.htm";
            HttpResponseMessage responce = await client.GetAsync(currentUrl); //Получаем ответ с сайта.
            string source = default;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
    }
}
