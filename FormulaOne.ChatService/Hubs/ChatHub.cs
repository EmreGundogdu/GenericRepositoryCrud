using FormulaOne.ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace FormulaOne.ChatService.Hubs;

public class ChatHub:Hub
{
    public async Task JoinChat(UserConnection userConnection)
    {
        await Clients.All.SendAsync("ReceiveMessage", "admin",$"{userConnection.UserName} has joined the chat");
    }

    public async Task JoinSpecificChatRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);
        await Clients.Group(userConnection.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin",$"{userConnection.UserName} has joined the chat {userConnection.ChatRoom}");
    }
}