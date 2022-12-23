using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Parser;
using ParserNewsSendTelegram.Telegram;


while (true)
{
    //парсим сайт https://meta.ua/news/all/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskmeta = Task.Factory.StartNew(() => ParserHtmlAgilityPack.GetNewsHtmlAgilityPack("//section//a[contains(@href, 'news')]", "https://meta.ua/news/all/", "https://meta.ua", new Proxys()));

    //парсим сайт https://ua.news/ua/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskUkrNet = Task.Factory.StartNew(() => ParserHtmlAgilityPack.GetNewsHtmlAgilityPack("//h3/parent::a", "https://ua.news/ua/", "https://ua.news", new Proxys()));

    //парсим новости с сайт https://news.online.ua/rss/ через rss  
    var taskOnlineUa = Task.Factory.StartNew(() => ParserXml.GetNewsParseXml("https://news.online.ua/rss/"));
    
    Task.WaitAll(taskOnlineUa, taskmeta, taskUkrNet);

    //Отправляем не опубликованные новости в телеграмм канал с бд
    var sendTg = SendTelegram.SendMessage();
    sendTg.Wait();
    Console.WriteLine("-----------------------------");
    Thread.Sleep(600000);//Пауза 10мин
}

 