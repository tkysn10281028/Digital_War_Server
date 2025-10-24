
using MagicOnion.Server.Hubs;
using Shared.IFs;

namespace Services
{
    public class GroupChatHub : StreamingHubBase<IGroupChatHub, IGroupChatHubReceiver>, IGroupChatHub
    {
        IGroup<IGroupChatHubReceiver>? group;
        string currentUserId = "";
        string currentGroupId = "";

        public async Task JoinGroupAsync(string userId, string groupId)
        {
            currentUserId = userId;
            currentGroupId = groupId;
            group = await Group.AddAsync(groupId);
            group.Except([Context.ContextId]).OnGroupUpdated(currentGroupId, userId, "joined");
        }

        public async Task LeaveGroupAsync(string userId)
        {
            await group!.RemoveAsync(Context);
            group.Except([Context.ContextId]).OnGroupUpdated(currentGroupId, userId, "left");
        }

        public async Task SendMessageAsync(string message)
        {
            group!.Except([Context.ContextId]).OnMessageReceived(currentGroupId, currentUserId, message);
        }
    }
}