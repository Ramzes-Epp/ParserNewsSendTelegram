using ParserNewsSendTelegram.Models;
using ParserNewsSendTelegram.Parser;
using ParserNewsSendTelegram.Telegram;

   
 
SendTelegram sendTelegram = new SendTelegram();

//парсим сайт https://meta.ua/news/all/ с помощью библиотеки Html Agility Pack и добавляем данные в бд
ParserHtmlAgilityPack parserHtmlAgilityPack = new ParserHtmlAgilityPack("//section//a[contains(@href, 'news')]", "https://meta.ua/news/all/", new Proxys());

// парсим сайт https://www.ukr.net/ajax/start.json через GET запрос - возращаеть обьект JSON
 
// парсим сайт https://news.online.ua/rss/ через rss возращаеть обьект XML

//Запускаем в цыкле парсеры и отправку новостей в телеграмм
while (true)
{
    var taskmeta2 = Task.Factory.StartNew(() => Console.WriteLine("taskmeta2 ЗАВЕРШЕН"));

    //запускаем парсинг сайт https://meta.ua/news/all/ в Task
    var taskmeta = Task.Factory.StartNew(() => parserHtmlAgilityPack.GetNews());
 
    var taskmeta3 = Task.Factory.StartNew(() => Console.WriteLine("taskmeta3 ЗАВЕРШЕН")); 
 
    Task.WaitAll(taskmeta, taskmeta2, taskmeta3); 
    Console.WriteLine("-----------------------------");
   // Thread.Sleep(600000);//Пауза 10мин
}

 