using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.Email.Model
{
    public class MessageTemplate
    {
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public required string ToAddress { get; set; }
        public required List<string> Bcc { get; set; }
        public required List<string> Cc { get; set; }
        public required EmailProperties EmailProperties { get; set; }
    }
}
