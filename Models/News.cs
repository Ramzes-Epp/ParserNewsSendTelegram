using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsSendTelegram.Models
{
    internal class News
    {
        internal Guid Id { get; set; }
        internal string TitleNews { get; set; }
        internal string UrlNews { get; set; }
        internal string UrlDonorNews { get; set; }
        internal string RubrikaNews { get; set; } = "No Category";
    }
}
