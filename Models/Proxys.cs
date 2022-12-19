using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserNewsSendTelegram.Models
{
    internal class Proxys
    { 
        internal string? userName { get; set; }
        internal string? password { get; set; } 
        internal string proxyHost { get; set; }
        internal int proxyPort { get; set; }
    }
}
