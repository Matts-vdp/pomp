using System;
using Microsoft.AspNetCore.SignalR;

namespace PompServer.Hubs;

public class UpdateHub : Hub
{
	public UpdateHub()
	{
        
    }
    public async Task SendMessage()
    {
        await Clients.All.SendAsync("update", "update");
    }

}
