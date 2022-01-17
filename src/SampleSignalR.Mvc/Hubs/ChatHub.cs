using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SampleSignalR.Mvc.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinGroup(string loteId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, loteId);

            await Clients.Group(loteId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {loteId}.");
        }

        public async Task ExitGroup(string loteId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, loteId);

            await Clients.Group(loteId).SendAsync("Send", $"{Context.ConnectionId} has left the group {loteId}.");
        }

        public async Task Send(string groupName, string name, string message)
        {
            await Clients.Group(groupName).SendAsync("sendMessageToGroup", name, message);
            // Call the broadcastMessage method to update clients.
            // Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}
