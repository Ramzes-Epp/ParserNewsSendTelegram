using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Parser;
using ParserNewsSendTelegram.Telegram; 

     
while (true)
{
    //парсим сайт https://meta.ua/news/all/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskmeta = Task.Factory.StartNew(() => ParserHtmlAgilityPack.GetNewsHtmlAgilityPack("//section//a[contains(@href, 'news')]", "https://meta.ua/news/all/", new Proxys()));

    //парсим сайт https://www.ukr.net с помощью библиотеки Html Agility Pack и добавляем данные в бд
    var taskUkrNet = Task.Factory.StartNew(() => ParserHtmlAgilityPack.GetNewsHtmlAgilityPack("//article//a[contains(@href, 'news') and @rel='nofollow']", "https://www.ukr.net/", new Proxys()));

    //парсим новости с сайт https://news.online.ua/rss/ через rss  
    var taskOnlineUa = Task.Factory.StartNew(() => ParserRss.GetNewsParseXml("https://news.online.ua/rss/"));
    
    Task.WaitAll(taskOnlineUa, taskmeta, taskUkrNet);

    //Отправляем не опубликованные новости в телеграмм канал
    //SendTelegram.SendMessage("https://meta.ua/");

    Console.WriteLine("-----------------------------");
    Thread.Sleep(600000);//Пауза 10мин
}

 