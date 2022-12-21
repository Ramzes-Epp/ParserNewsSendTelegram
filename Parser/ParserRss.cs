using HtmlAgilityPack;
using ParserNewsSendTelegram.Data;
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

            //перебираем коллекцию и добавляем в БД данные
            foreach (var item in items)
            { 
                SqliteServis.AddNews(new News { TitleNews = item.Element("title").Value, LinkNews = item.Element("link").Value, UrlDonorNews = url });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("НЕ ПОЛУЧИЛОСЬ спарсить  " + url + " " + ex.Message);
        }
    }
}

