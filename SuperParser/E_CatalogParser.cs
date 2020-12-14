//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SuperParser
//{
//    public class Class1
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace SuperParser
{
    public class E_CatalogParser : IParser
    {
        public List<string> list;
        public List<string> list1;
        public List<string> list3;
        public List<string> Parse(IHtmlDocument document)
        {
            list = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("a")
                .Where(item => item.ClassName != null && item.ClassName.Contains("model-short-title no-u"));
            foreach (var item in items)
            {
                list.Add(item.GetAttribute("href"));
                //list3.Add(item.TextContent);
            }

            return list;
        }

        public List<string> Parse1(IHtmlDocument document)
        {
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("td")
                .Where(item => item.ClassName != null && item.ClassName.Equals("op3"));

            foreach (var item in items)
            {
                list1.Add(item.TextContent);
            }

            return list1;
        }
        public List<string> ParsePrice(IHtmlDocument document)
        {
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Equals("model-price-range"));

            foreach (var item in items)
            {
                list1.Add(item.TextContent);
            }

            return list1;
        }
    }
}

