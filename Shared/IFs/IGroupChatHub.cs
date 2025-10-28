using MagicOnion;
using Shared.Dtos;

namespace Shared.IFs
{
    public interface IGroupChatHub : IStreamingHub<IGroupChatHub, IGroupChatHubReceiver>
    {
        Task JoinGroupAsync(string userId, string groupId);
        Task LeaveGroupAsync();
        Task SendMessageAsync(ChatMessage chatMessage);
    }

    public interface IGroupChatHubReceiver
    {
        void OnMessageReceived(ChatMessage chatMessage);
        void OnGroupUpdated(ChatMessage chatMessage);
    }
}