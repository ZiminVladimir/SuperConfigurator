using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperParser
{
    class E_CatalogSettings : IParserSettings
    {
        public E_CatalogSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://www.e-katalog.ru/list/189/";
        public string Postfix { get; set; } = "{CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}

