using ParserNewsSendTelegram.Data;

namespace ParserNewsSendTelegram.Telegram;

internal class SendTelegram
{
    static readonly string token = "";
    static readonly string chatId = "";

    /// <summary>
    /// отправка сообщения со ссылкой в телеграмм
    /// </summary>
    internal static async Task SendMessage()
    {
        var listNews = SqliteServis.GetAllNews();

        foreach (var news in listNews)
        {
            if (news.OpublikovanTelegram == "yes")
                continue;
            Console.WriteLine("отправл¤ем новость в тг " + news.LinkNews);
            try
            {
                using HttpClient client = new HttpClient();
                var url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={news.LinkNews}";
                HttpResponseMessage response = await client.GetAsync(url);
                Console.WriteLine(response.IsSuccessStatusCode + "  LinkNews = " + news.LinkNews);
                // изменяем в бд что опубликовали
                if (response.IsSuccessStatusCode)
                    SqliteServis.UpdateNews(news.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ќ≈ ѕќЋ”„»Ћќ—№ отправить в телеграмм  " + ex.Message);
            }
        }
    }
}
