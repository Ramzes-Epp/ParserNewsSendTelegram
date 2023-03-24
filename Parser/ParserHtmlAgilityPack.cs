using HtmlAgilityPack;
using ParserNewsSendTelegram.Data;
using ParserNewsSendTelegram.Models;
using System.Net;

namespace ParserNewsSendTelegram.Parser;

/// <summary>
/// парсинг сайта с помощью Html Agility Pack  
/// </summary>
internal class ParserHtmlAgilityPack
{
    /// <summary>
    /// парсим сайт с помощью Html Agility Pack
    /// </summary>
    /// <param name="xPath"></param>
    /// <param name="url"></param>
    /// <param name="domenDonora"></param>
    /// <param name="proxy"></param>
    /// <returns></returns>
    internal async Task GetNewsHtmlAgilityPack(string xPath, string url, string domenDonora, string proxy = "")
    {
        using WebClient client = new WebClient();
        if (proxy != "")//Устанавливаем прокси если есть
            client.Proxy = new WebProxy(proxy, true);

        try
        {
            // Загружаем HTML-код с указанного URL-адреса
            string htmlCode = await client.DownloadStringTaskAsync(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);

            var node = htmlDoc.DocumentNode.SelectNodes(xPath);

            //перебираем коллекцию и добавляем в БД данные
            foreach (var item in node)
            {
                var strHref = item.GetAttributeValue("href", null);

                if (!strHref.Contains(domenDonora))//если домена в урле нет то добовляем
                    strHref = domenDonora + strHref;

                SqliteServis.AddNews(new News { TitleNews = item.InnerText, LinkNews = item.GetAttributeValue("href", null), UrlDonorNews = url });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("GetNewsHtmlAgilityPack - не спарсили  " + url + " " + ex.Message);
        }
    }

}

