using HtmlAgilityPack;
using ParserNewsSendTelegram.Data;
using ParserNewsSendTelegram.Models;
using System.Text;

namespace ParserNewsSendTelegram.Parser;

/// <summary>
/// парсим сайт с помощью Html Agility Pack
/// </summary>
internal class ParserHtmlAgilityPack
{
    internal static void GetNewsHtmlAgilityPack(string xPath, string url, string domenDonora,  Proxys proxy)
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

            //перебираем коллекцию и добавляем в БД данные
            foreach (var item in node)
            {
                var strHref = item.GetAttributeValue("href", null);

                if (!strHref.Contains(domenDonora))//если домена в урле нет то добовлянм
                    strHref = domenDonora + strHref;

                SqliteServis.AddNews(new News { TitleNews = item.InnerText, LinkNews = item.GetAttributeValue("href", null), UrlDonorNews = url});
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("НЕ ПОЛУЧИЛОСЬ спарсить  " + url + " " + ex.Message);
        }
    }

}

