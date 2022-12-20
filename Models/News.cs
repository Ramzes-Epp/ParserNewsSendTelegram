namespace ParserNewsSendTelegram.Models
{
    internal class News
    {
        internal Guid Id { get; set; }
        internal string TitleNews { get; set; }
        internal string UrlDonorNews { get; set; }
        internal string RubrikaNews { get; set; } = "No Category";
        internal bool OpublikovanTelegram { get; set; } = false;
    }
}
