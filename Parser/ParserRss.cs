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
    /// ������ Rss ����� �����  
    /// </summary>
    internal static void GetNewsParseXml(string url)
    {
        try
        {
            using WebClient client = new WebClient();
            var htmlCode = client.DownloadString(url);//�������� html ��������

            XElement ParsElement = XElement.Parse(htmlCode);//������������� � XMl �������
            var items = ParsElement.Elements("channel").Elements("item");//�������� ���� � ������� ����� title � link  

            //���������� ��������� � ��������� � �� ������
            foreach (var item in items)
            { 
                SqliteServis.AddNews(new News { TitleNews = item.Element("title").Value, LinkNews = item.Element("link").Value, UrlDonorNews = url });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("�� ���������� ��������  " + url + " " + ex.Message);
        }
    }
}

