using Microsoft.AspNetCore.SignalR;

namespace EventManagement.Hubs
{
    public class EventHub : Hub
    {
        public async Task SendAsync(string method)
        {
            await Clients.All.SendAsync(method);
        }
    }
}
