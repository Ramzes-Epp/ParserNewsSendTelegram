using HtmlAgilityPack;
using ParserNewsSendTelegram.Models;
using System.Text;

namespace ParserNewsSendTelegram.Parser
{
    /// <summary>
    /// парсим сайт с помощью Html Agility Pack
    /// </summary>
    internal class ParserHtmlAgilityPack
    {  
        internal void GetNewsHtmlAgilityPack(string xPath, string url, Proxys proxy)
        {
            HtmlWeb web = new HtmlWeb(); 
            web.OverrideEncoding = Encoding.UTF8;
            HtmlDocument htmlDoc = new HtmlDocument();

            try
            {
                //Устанавливаем прокси если есть
                // TODO - прописать рандомный выбор (и чек прокси) с пула проксей
                if (proxy.proxyPort != 0)
                    htmlDoc.LoadHtml(web.Load(url, proxy.proxyHost, proxy.proxyPort, proxy.userName, proxy.password).Text);
                else
                     htmlDoc.LoadHtml(web.Load(url).Text);// получаем HtmlDocument 
                 
                var node = htmlDoc.DocumentNode.SelectNodes(xPath);

                foreach (var item in node)
                { 
                    Console.WriteLine(item.InnerText); 
                    Console.WriteLine(item.GetAttributeValue("href", null));
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("НЕ ПОЛУЧИЛОСЬ спарсить  " + url + " " + ex.Message);
            }
        }
         
    }
}
