Создано 3 варианта парсинга сайтов:
1 - с помощью библиотеки Html Agility Pack 
2 - через GET запрос - возвращает объект JSON
3 - через rss возвращает объект XML

Подключена БД SQLite и в нее добавляються новые новости, поля в БД
 Guid Id 
 string TitleNews  
 string UrlDonorNews 
 string RubrikaNews 
 bool OpublikovanTelegram 
		
		
Отправка новых новостей в Телеграмм.

Сайты новостей с которыми работаем:
Сайт https://meta.ua/news/all/ парсинг с помощью библиотеки Html Agility Pack
Сайт https://www.ukr.net/ajax/start.json через GET запрос - парсинг обьекта JSON
Сайт https://news.online.ua/rss/ через rss парсинг обьект XML

Шаблон унифицирован  и в Program.cs легко можно подключить новые сайты.



