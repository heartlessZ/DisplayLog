using DisplayLog.Models;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace DisplayLog.Extensions
{
    public static class DisplayLogExtension
    {

        public static LoggerConfiguration WriteToDisplayLogWeb(this LoggerConfiguration config ,string displayLogWebUrl,LogOptions options=null)
        {
            options = options ?? new LogOptions();
            var logSink = new LogWebSink(displayLogWebUrl, options);
            config.WriteTo.Sink(logSink, restrictedToMinimumLevel: options.MinLevel);

            return config;
        }
    }

    public class LogWebSink : ILogEventSink
    {
        private readonly string logWebUrl;
        private readonly LogOptions logOptions;
        readonly ITextFormatter _textFormatter;
        private HttpClient httpClient = new HttpClient();

        public LogWebSink(string displayLogWebUrl, LogOptions options) {
            logWebUrl = displayLogWebUrl;
            logOptions = options;
            _textFormatter = new MessageTemplateTextFormatter(options.OutPutTemplate, null);
        }


        /// <summary>
        /// 日志事件
        /// </summary>
        public static Func<string, bool> LogFunc = null;

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) return;

            string log;
            using (var renderSpace = new StringWriter())
            {
                _textFormatter.Format(logEvent, renderSpace);
                log = renderSpace.ToString();
            }

            if (string.IsNullOrWhiteSpace(log)) return;

            var url = logWebUrl + "/api/Log/UploadLog";
            httpClient.PostAsJsonAsync(url, new LogMessage()
            {
                AppId = logOptions.Id,
                ClientName = logOptions.Name,
                Content = log
            });
        }
    }
}
