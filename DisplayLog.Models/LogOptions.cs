using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayLog.Models
{
    public class LogOptions
    {
        public LogOptions()
        {
            Id = Guid.NewGuid();
            Name = AppDomain.CurrentDomain.FriendlyName;
            OutPutTemplate = "{Timestamp} [{Level}] {Message}{Exception}";
            MinLevel = Serilog.Events.LogEventLevel.Debug;
        }

        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string OutPutTemplate { get; set; }

        public Serilog.Events.LogEventLevel MinLevel { get; set; }
    }
}
