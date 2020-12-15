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
        public List<string> list4;
        public List<string> Parse(IHtmlDocument document)
        {
            list = new List<string>();
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("a")
                .Where(item => item.ClassName != null && item.ClassName.Contains("model-short-title no-u"));
            foreach (var item in items)
            {
                list.Add(item.GetAttribute("href"));               
            }

            return list;
        }
        public List<string> ParseName(IHtmlDocument document)
        {
            list3 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("a")
                .Where(item => item.ClassName != null && item.ClassName.Contains("model-short-title no-u"));
            foreach (var item in items)
            {
                list3.Add(item.TextContent);
            }

            return list3;
        }

        public List<string> Parse1(IHtmlDocument document)
        {
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("td")
                .Where(item => item.ClassName != null && item.ClassName.Contains("op3"));

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
        public List<string> ParseCategories(IHtmlDocument document)
        {
            list4 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("span")
                .Where(item => item.ClassName != null && item.ClassName.Equals("gloss"));

            foreach (var item in items)
            {
                list4.Add(item.TextContent);
            }

            return list4;
        }
        public List<string> ParseRAM(IHtmlDocument document)
        {
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("td")
                .Where(item => item.ClassName != null && item.ClassName.Equals("val"));

            foreach (var item in items)
            {
                list1.Add(item.TextContent);
            }

            return list1;
        }
        public List<string> ParseMB(IHtmlDocument document)
        {
            list1 = new List<string>();
            IEnumerable<IElement> items = document.QuerySelectorAll("div")
                .Where(item => item.ClassName != null && item.ClassName.Contains("list-more-div-small blue-button h"));

            foreach (var item in items)
            {
                list1.Add(item.GetAttribute("jsource"));
            }

            return list1;
        }
    }
}

