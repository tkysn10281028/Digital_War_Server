using Shared.Dtos;
using Shared.IFs;

namespace RealtimeClientTest.RealtimeClientBroadcast.Services
{
    public class GroupChatHubReceiver : IGroupChatHubReceiver
    {
        public void OnGroupUpdated(ChatMessage chatMessage)
        {
            Console.WriteLine($"User: {chatMessage.UserId} {chatMessage.Message} the Group: {chatMessage.GroupId} !");
        }

        public void OnMessageReceived(ChatMessage chatMessage)
        {
            Console.WriteLine($"User: {chatMessage.UserId} Sent a message : ' {chatMessage.Message} ' to the Group: {chatMessage.GroupId} !");
        }
    }
}