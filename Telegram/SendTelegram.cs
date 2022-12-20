using System.Net;

namespace ParserNewsSendTelegram.Telegram;

internal class SendTelegram
{
    static readonly string token = "5956476617:AAFO9WMnWRA6TmpkRTOw1Jl7RQe0SpTm0KU";
    static readonly string chatId = "-1001309999215";

    public static string SendMessage(string message)
    {
        string retval = string.Empty;
        string url = $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text={message}";

        using (var webClient = new WebClient())
        {
            retval = webClient.DownloadString(url);
        }
        Console.WriteLine(retval);
        return retval;
    }
}
