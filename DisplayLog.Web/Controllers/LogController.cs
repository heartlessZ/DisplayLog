using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DisplayLog.Models;
using DisplayLog.Services.Log;
using DisplayLog.Web.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace DisplayLog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private IHubContext<PushLogHub> logHub;
        private AppConfig appConfig;
        private ILogService logService;

        public LogController(IHubContext<PushLogHub> hubContext, IOptions<AppConfig> options, ILogService service)
        {
            logHub = hubContext;
            appConfig = options.Value;
            logService = service;
        }

        /// <summary>
        /// 发送日志到连接的客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost("UploadLog")]
        public async Task UploadLog([FromBody] LogMessage client)
        {
            await logHub.Clients.All.SendCoreAsync("ReciveMessage", new object[] { client});
        }

        /// <summary>
        /// 根据文件夹名称获取指定目录下的所有日志文件
        /// </summary>
        /// <param name="clientName">客户端名称</param>
        /// <returns></returns>
        [HttpGet("GetFilesName")]
        public IActionResult GetFilesName(string clientName)
        {
            return Ok(logService.GetFilesName(clientName));
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="clientName">客户端名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpGet("GetFile")]
        public IActionResult GetFileByClientName(string clientName, string fileName)
        {
            var buffer = logService.GetBuffer(clientName, fileName);
            var extension = Path.GetExtension(fileName);
            if (buffer == null)
            {
                return null;
            }
            return File(buffer, FileContentType.GetMimeType(extension));
        }
    }
}
