using System;

namespace DisplayLog.Models
{
    public class LogMessage
    {
        public string Content { get; set; }

        public string ClientName { get; set; }

        public Guid AppId { get; set; }
    }
}
