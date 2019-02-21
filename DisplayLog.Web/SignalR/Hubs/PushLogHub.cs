using DisplayLog.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisplayLog.Web.SignalR.Hubs
{
    public class PushLogHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(LogMessage log)
        {
            await Clients.All.SendAsync("ReciveMessage", log);
        }
    }
}
