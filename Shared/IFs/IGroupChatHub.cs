using MagicOnion;

namespace Shared.IFs
{
    public interface IGroupChatHub : IStreamingHub<IGroupChatHub, IGroupChatHubReceiver>
    {
        Task JoinGroupAsync(string userId, string groupId);
        Task LeaveGroupAsync(string userId);
        Task SendMessageAsync(string message);
    }

    public interface IGroupChatHubReceiver
    {
        void OnMessageReceived(string groupId, string userId, string message);
        void OnGroupUpdated(string groupId, string userId, string message);
    }
}