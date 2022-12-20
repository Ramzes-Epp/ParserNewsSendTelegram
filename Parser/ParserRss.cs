using HtmlAgilityPack;
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

            //���������� ��������� � ����������� ������ � title
            foreach (var item in items)
            {
                Console.WriteLine(item.Element("title").Value);
                Console.WriteLine(item.Element("link").Value);
             //   SendTelegram.SendMessage(item.Element("link").Value);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("�� ���������� ��������  " + url + " " + ex.Message);
        }
    }
}

