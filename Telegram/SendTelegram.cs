using ParserNewsSendTelegram.Data;
using System.Net;
using System.Security.Policy;

namespace ParserNewsSendTelegram.Telegram;

internal class SendTelegram
{
    //���� ��� ����� ����� ������� � ��������� ��������, ������� ����� ��� �� �� ����� ��������� ���������� 
    static readonly string token = "5956476617:AAFO9WMnWRA6TmpkRTOw1Jl7RQe0SpTm0KU";
    static readonly string chatId = "-1001309999215";

    /// <summary>
    /// �������� � �� ��������� �� �������
    /// </summary>
    internal static void SendMessage()
    { 
        var listNews = SqliteServis.GetAllNews();
        foreach (var news in listNews)
        {
            if (news.OpublikovanTelegram == "yes")
                continue;
            try
            {
                string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={news.LinkNews}";
                using var webClient = new WebClient();
                webClient.DownloadString(url);

                //�������� � �� ��� ������������ 
                SqliteServis.UpdateNews(news.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("�� ���������� ��������� � ���������  " + ex.Message);
            } 
        } 
    }
}
