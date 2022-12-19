using HtmlAgilityPack;
using ParserNewsSendTelegram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsSendTelegram.Parser
{
    /// <summary>
    /// парсим сайт с помощью Html Agility Pack
    /// </summary>
    internal class ParserHtmlAgilityPack
    {
        readonly string _xPath;
        readonly string _url;
        readonly Proxys _proxy;
        public ParserHtmlAgilityPack(string xPath, string url, Proxys proxy)
        {
            _xPath = xPath;
            _url = url;
            _proxy = proxy;
        }

        internal void GetNews()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();

            try
            {
                //Устанавливаем прокси если есть
                // TODO - прописать рандомный выбор (и чек прокси) с пула проксей
                if (_proxy.proxyPort != 0)
                    htmlDoc.LoadHtml(web.Load(_url, _proxy.proxyHost, _proxy.proxyPort, _proxy.userName, _proxy.password).Text);
                else
                     htmlDoc.LoadHtml(web.Load(_url).Text); // библиотека HtmlAgilityPack асинхронно получает HtmlDocument 

                var node = htmlDoc.DocumentNode.SelectNodes(_xPath);

                foreach (var item in node)
                {
                    Console.WriteLine(item.InnerText);
                    Console.WriteLine(item.GetAttributeValue("href", null));
                }
                Console.WriteLine(node);
            }
            catch (Exception)
            {
                Console.WriteLine("НЕ ПОЛУЧИЛОСЬ спарсить " + _url);
            }
        }
         
    }
}
