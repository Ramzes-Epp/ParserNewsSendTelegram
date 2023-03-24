using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
namespace ParserNewsSendTelegram.Parser;

/// <summary>
/// парсер Api сайта Json
/// </summary>
internal class ParserApi
{
    /// <summary>
    /// парсер Api сайта ukr.net - через GET запрос
    /// </summary>
    internal async Task GetNews(string proxy = "")
    {
        // парсим сайт https://www.ukr.net/ajax/start.json через GET запрос - возращаеть обьект JSON  
        string url = @"https://www.ukr.net/ajax/start.json";
        try
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.Proxy = new WebProxy(proxy, true);//Устанавливаем прокси если есть

            using HttpClient httpClient = new HttpClient(httpClientHandler);

            string requestBody = "";
            using var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Accept-Encoding", "deflate, br");
            request.Headers.Add("Host", "www.ukr.net");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:108.0) Gecko/20100101 Firefox/108.0");

            var resultresponse = await httpClient.SendAsync(request);

            if (resultresponse.IsSuccessStatusCode)
            {
                string resultGet = Regex.Unescape(resultresponse.Content.ReadAsStringAsync().Result); //декодируем с Unicode

                resultGet = resultGet.Replace("\"", "");
                Console.WriteLine(resultGet);

                if (!string.IsNullOrEmpty(resultGet))
                {
                    List<object> items = JsonConvert.DeserializeObject<List<object>>(resultGet);
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("GetNews - не спарсили  " + url + " " + ex.Message);
        }
    }
}

