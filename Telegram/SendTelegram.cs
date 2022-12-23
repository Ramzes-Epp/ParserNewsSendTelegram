using ParserNewsSendTelegram.Data;

namespace ParserNewsSendTelegram.Telegram;

internal class SendTelegram
{
    //���� ��� ����� ����� ������� � ��������� ��������, ������� ����� ��� �� �� ����� ��������� ���������� 
    static readonly string token = "5956476617:AAFO9WMnWRA6TmpkRTOw1Jl7RQe0SpTm0KU";
    static readonly string chatId = "-1001309999215";

    /// <summary>
    /// �������� � �� ��������� �� �������
    /// </summary>
    internal static async Task SendMessage()
    {
        var listNews = SqliteServis.GetAllNews();

        foreach (var news in listNews)
        {
            if (news.OpublikovanTelegram == "yes")
                continue;
            Console.WriteLine("���������� ������� � �� " + news.LinkNews); 
            try
            {
                using HttpClient client = new HttpClient();
                var url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={news.LinkNews}";
                HttpResponseMessage response = await client.GetAsync(url);
                Console.WriteLine(response.IsSuccessStatusCode + "  LinkNews = " + news.LinkNews);
                // �������� � �� ��� ������������
                if (response.IsSuccessStatusCode)
                    SqliteServis.UpdateNews(news.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("�� ���������� ��������� � ���������  " + ex.Message);
            } 
        }
    }
}
