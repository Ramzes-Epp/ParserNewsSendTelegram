using HtmlAgilityPack;
using ParserNewsSendTelegram.Models;
using System.Net;
using System.Xml.Linq;

namespace ParserNewsSendTelegram.Parser
{
    internal class ParserRss
    { 
        internal void GetNewsParseXml(string url)
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ќ≈ ѕќЋ”„»Ћќ—№ спарсить  " + url + " " + ex.Message);
            }
        }
    }
}
