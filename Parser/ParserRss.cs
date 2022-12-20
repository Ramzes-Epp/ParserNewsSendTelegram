using HtmlAgilityPack;
using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Telegram;
using System.Net;
using System.Xml.Linq;

namespace ParserNewsSendTelegram.Parser;

internal class ParserRss
{
    /// <summary>
    /// парсер Rss ленты сайта  
    /// </summary>
    internal static void GetNewsParseXml(string url)
    {
        try
        {
            using WebClient client = new WebClient();
            var htmlCode = client.DownloadString(url);//получили html страницы

            XElement ParsElement = XElement.Parse(htmlCode);//преобразовали в XMl елемент
            var items = ParsElement.Elements("channel").Elements("item");//получаем блок в котором лежат title и link  

            //перебераем коллекцию и вытаскиваем ссылки и title
            foreach (var item in items)
            {
                Console.WriteLine(item.Element("title").Value);
                Console.WriteLine(item.Element("link").Value);
             //   SendTelegram.SendMessage(item.Element("link").Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ќ≈ ѕќЋ”„»Ћќ—№ спарсить  " + url + " " + ex.Message);
        }
    }
}

