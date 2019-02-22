using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisplayLog.Models;
using DisplayLog.Web.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DisplayLog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private IHubContext<PushLogHub> logHub;
        public LogController(IHubContext<PushLogHub> hubContext)
        {
            logHub = hubContext;
        }

        [HttpPost("UploadLog")]
        public async Task UploadLog([FromBody] LogMessage client)
        {
            await logHub.Clients.All.SendCoreAsync("ReciveMessage", new object[] { client});
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "value";
        }
    }
}
