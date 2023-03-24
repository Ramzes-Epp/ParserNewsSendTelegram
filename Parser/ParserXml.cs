using ParserNewsSendTelegram.Data;
using ParserNewsSendTelegram.Models;
using System.Net;
using System.Xml.Linq;

namespace ParserNewsSendTelegram.Parser;
/// <summary>
/// парсер Rss ленты сайта 
/// </summary>
internal class ParserXml
{
    /// <summary>
    /// парсер Rss ленты сайта через XML 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="proxy"></param>
    /// <returns></returns>
    internal async Task GetNewsParseXml(string url, string proxy = "")
    {
        try
        {
            using WebClient client = new WebClient();
            if (proxy != "")
                client.Proxy = new WebProxy(proxy, true);

            var htmlCode = await client.DownloadStringTaskAsync(url); //получили html страницы

            XElement ParsElement = XElement.Parse(htmlCode);//преобразовали в XMl елемент
            var items = ParsElement.Elements("channel").Elements("item");//получаем блок в котором лежат title и link  

            //перебираем коллекцию и добавл¤ем в бд данные
            foreach (var item in items)
            {
                SqliteServis.AddNews(new News { TitleNews = item.Element("title").Value, LinkNews = item.Element("link").Value, UrlDonorNews = url });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(" GetNewsParseXml - не спарсили " + url + " " + ex.Message);
        }
    }
}

