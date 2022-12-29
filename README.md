ЗАДАНИЕ
Создайте программу на C#, которая каждые 10мин будет парсить страницы новостных сайтов (на ваш выбор), и добавлять новые записи в БД SQLite.
Из БД записи отправить в телеграм канал. 

Стек:
	
	C#
	
	Html Agility Pack
	
	SQLite
	
	API телеграм	
 


РЕШЕНИЕ
Создано 3 варианта парсинга сайтов:
	
	1 - с помощью библиотеки Html Agility Pack и xPatch
	
	2 - через GET запрос - возвращает объект JSON
	
	3 - через GET запрос - возвращает объект XML (например rss ленту или sitemap.xml)

Подключена БД SQLite и в нее добавляються новые новости, поля в БД
 Guid Id 
 string TitleNews  
 string UrlDonorNews 
 string RubrikaNews 
 bool OpublikovanTelegram 
		
		
Асинхронная отправка новых новостей в Телеграмм канал @news_k_h, (ссылка на канал https://t.me/+pFPSnJEo3TQwZjky )

Сайты новостей с которыми работаем:

Сайт https://meta.ua/news/all/ парсинг с помощью библиотеки Html Agility Pack 

Сайт https://ua.news/ua/ парсинг с помощью библиотеки Html Agility Pack 

Сайт https://news.online.ua/rss/ через rss ленту парсинг обьекта XML

Шаблон унифицирован и в Program.cs легко можно подключить новые сайты.



