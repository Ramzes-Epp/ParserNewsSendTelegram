using ParserNewsSendTelegram.Models;
using System.Data.SQLite;

namespace ParserNewsSendTelegram.Data
{
    internal class SqliteServis
    {
        readonly static string connection_string = "Data Source=" + Environment.CurrentDirectory + @"\dbnews.sqlite;Version=3;";
        
        /// <summary>
        /// Получить в список все новости
        /// </summary>
        /// <returns></returns>
        internal static List<News> GetAllNews()
        { 
            try
            {
                using (var connection = new SQLiteConnection(connection_string))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(@"SELECT id, title_news, url_donor_news, link_news, date_created, opublikovan_telegram FROM news;", connection))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            List<News> listNews = new List<News>();

                            while (rdr.Read())
                            {
                                listNews.Add(new News
                                {
                                    Id = rdr.GetInt32(0),
                                    TitleNews = rdr.GetString(1),
                                    UrlDonorNews = rdr.GetString(2),
                                    LinkNews = rdr.GetString(3),
                                    DateNews = rdr.GetString(4),
                                    OpublikovanTelegram = rdr.GetString(5) 
                                });
                            }

                            return listNews;
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return null;
        }


        /// <summary>
        /// Добавить в бд новость
        /// </summary>
        /// <param name="news"> новость </param>
        /// <returns></returns>
        internal static bool AddNews(News news)
        {
            try
            {
                using (var connection = new SQLiteConnection(connection_string))
                {
                    connection.Open();
                     
                    using (var cmd = new SQLiteCommand("INSERT INTO news (title_news, url_donor_news, " +
                        "link_news, date_created, opublikovan_telegram) " +
                        $@"VALUES('{news.TitleNews}', '{news.UrlDonorNews}', '{news.LinkNews}', '{DateTime.Now.ToString("dd-MM-yyyy")}', '{news.OpublikovanTelegram}'); ", connection))
                    {
                        var res = cmd.ExecuteNonQuery();
                        return true;
                    } 
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }


        /// <summary>
        /// Изменяем в БД что опубликовали ссылку в ТГ
        /// </summary>
        /// <param name="idNews"> id ссылки</param>
        /// <returns></returns>
        internal static bool UpdateNews(int idNews = -1)
        {
            if (idNews == -1)
                return false;
            try
            {
                using (var connection = new SQLiteConnection(connection_string))
                {
                    connection.Open();
                     
                    using (var cmd = new SQLiteCommand($@"UPDATE news SET opublikovan_telegram = 'yes' WHERE id = '{idNews}'", connection))
                    {
                        var res = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        } 
    }
}
