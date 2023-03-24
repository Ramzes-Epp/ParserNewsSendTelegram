using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Parser;
using ParserNewsSendTelegram.Telegram;

while (true)
{
    //парсим сайт https://meta.ua/news/all/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskmeta = Task.Run(async () =>
   {
       ParserHtmlAgilityPack parserHtmlAgilityPack = new ParserHtmlAgilityPack();
       await parserHtmlAgilityPack.GetNewsHtmlAgilityPack("//section//a[contains(@href, 'news')]", "https://meta.ua/news/all/", "https://meta.ua");
   });


    //парсим сайт https://ua.news/ua/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskUkrNet = Task.Run(async () =>
   {
       ParserHtmlAgilityPack parserHtmlAgilityPack = new ParserHtmlAgilityPack();
       await parserHtmlAgilityPack.GetNewsHtmlAgilityPack("//h3/parent::a", "https://ua.news/ua/", "https://ua.news");
   });


    //парсим новости с сайт https://news.online.ua/rss/ через rss    
    var taskOnlineUa = Task.Run(async () =>
    {
        ParserXml parserXml = new ParserXml();
        await parserXml.GetNewsParseXml("https://news.online.ua/rss/");
    });

    Task.WaitAll(taskOnlineUa, taskmeta, taskUkrNet);

    //Отправляем не опубликованные новости в телеграмм канал с бд
    var sendTg = SendTelegram.SendMessage();
    sendTg.Wait();
    Console.WriteLine("-----------------------------");
    Thread.Sleep(600000);//Пауза 10мин
}

