/****************************************************************
* MACHINE: DESKTOP-KFVHQEM
* NAME: boomclub
* CREATEDATE: 2019/2/27 10:28:43
* DESC: <DESCRIPTION>
* **************************************************************/
using DisplayLog.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLog.Services.Log
{
    /// <summary>
    /// Summary description for LogService
    /// </summary>
    public class LogService : ILogService
    {
        private AppConfig appConfig;

        public LogService(IOptions<AppConfig> options)
        {
            appConfig = options.Value;
        }

        public byte[] GetBuffer(string clientName, string fileName)
        {
            var path = Path.Combine(appConfig.LogFilePath, clientName);
            if (!Directory.Exists(path))
            {
                return null;
            }
            var filePath = Path.Combine(path, fileName);
            if (!File.Exists(filePath))
            {
                return null;
            }
            return File.ReadAllBytes(filePath);
        }

        public List<string> GetFilesName(string clientName)
        {
            var fileNames = new List<string>();
            var path = Path.Combine(appConfig.LogFilePath, clientName);
            if (!Directory.Exists(path))
            {
                throw new Exception("The directory not found.");
            }
            foreach (var file in Directory.GetFiles(path))
            {
                fileNames.Add(Path.GetFileName(file));
            }
            return fileNames;
        }
    }
}
