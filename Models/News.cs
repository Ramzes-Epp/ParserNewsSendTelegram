namespace ParserNewsSendTelegram.Models;

internal class News
{
    internal int Id { get; set; } = 0;
    internal string TitleNews { get; set; }
    internal string LinkNews { get; set; }
    internal string? UrlDonorNews { get; set; }
    internal string? RubrikaNews { get; set; } = "No Category";
    internal string OpublikovanTelegram { get; set; } = "no"; 
    internal string? DateNews { get; set; }
}
