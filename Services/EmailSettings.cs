using System.Collections.Generic;

namespace Services
{
    public class EmailSettings
    {
        public string? MailAccount { get; set; }
        public string? MailServer { get; set; }
        public int? MailPort { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public string? Password { get; set; }
    }

    public class EmailOption
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? UrlToken { get; set; }
    }
}