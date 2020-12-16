using AngleSharp.Html.Dom;
using System.Collections.Generic;
namespace SuperParser
{
    public interface IParser
    {
        List<string> Parse(IHtmlDocument document);
        List<string> Parse1(IHtmlDocument document);
        List<string> ParsePrice(IHtmlDocument document);
        List<string> ParseName(IHtmlDocument document);
        List<string> ParseCategories(IHtmlDocument document);
        List<string> ParseRAM(IHtmlDocument document);
        List<string> ParseMB(IHtmlDocument document);
        List<string> ParseNameBlue(IHtmlDocument document);
    }
}
