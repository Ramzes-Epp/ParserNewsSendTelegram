using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace ParserNewsSendTelegram.Parser;

internal class ParserApi
{
    /// <summary>
    /// парсер Api сайта - через GET запрос
    /// </summary>
    internal void GetNews()
    {
        // парсим сайт https://www.ukr.net/ajax/start.json через GET запрос - возращаеть обьект JSON  
        string url = @"https://www.ukr.net/ajax/start.json";
        try
        {
            using HttpClient httpClient = new HttpClient();
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

            var resultresponse = httpClient.SendAsync(request).Result;

            if (resultresponse.IsSuccessStatusCode)
            {
                string resultGet = Regex.Unescape(resultresponse.Content.ReadAsStringAsync().Result); //декодируем с Unicode
                                                                                                      //TODO выпарсить с помощью регул€рок данные с Json (resultGet) т.к сайт не коретно отдает данные (Json не полный)
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
            Console.WriteLine("Ќ≈ ѕќЋ”„»Ћќ—№ спарсить  " + url + " " + ex.Message);
        }
    }
}

