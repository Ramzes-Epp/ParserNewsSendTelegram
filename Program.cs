using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Parser;
using ParserNewsSendTelegram.Telegram; 

    
SendTelegram sendTelegram = new SendTelegram();

//парсим сайт https://meta.ua/news/all/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
ParserHtmlAgilityPack parserHtmlAgilityPackMetaUa = new ParserHtmlAgilityPack();

//парсим сайт https://www.ukr.net с помощью библиотеки Html Agility Pack и добавляем данные в бд
ParserHtmlAgilityPack parserHtmlAgilityPackUkrNet = new ParserHtmlAgilityPack();
 
// парсим новости с сайт https://news.online.ua/rss/ через rss  
ParserRss ParserRssOnlineUa = new ParserRss();

 
while (true)
{  
    //запускаем парсинг сайт https://meta.ua/news/all/ в Task
    var taskmeta = Task.Factory.StartNew(() => parserHtmlAgilityPackMetaUa.GetNewsHtmlAgilityPack("//section//a[contains(@href, 'news')]", "https://meta.ua/news/all/", new Proxys()));
    
    //запускаем парсинг сайт https://www.ukr.net в Task
    var taskUkrNet = Task.Factory.StartNew(() => parserHtmlAgilityPackUkrNet.GetNewsHtmlAgilityPack("//article//a[contains(@href, 'news') and @rel='nofollow']", "https://www.ukr.net/", new Proxys()));
    
    //запускаем парсинг сайт https://news.online.ua в Task
    var taskOnlineUa = Task.Factory.StartNew(() => ParserRssOnlineUa.GetNewsParseXml("https://news.online.ua/rss/"));
    
    Task.WaitAll(taskOnlineUa, taskmeta, taskUkrNet);
     
    Console.WriteLine("-----------------------------");
    Thread.Sleep(600000);//Пауза 10мин
}

 