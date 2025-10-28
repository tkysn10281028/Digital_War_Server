
using MagicOnion.Server.Hubs;
using Shared.Dtos;
using Shared.IFs;

namespace RealtimeServer.Services
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

            group.Except([Context.ContextId]).OnGroupUpdated(new ChatMessage
            {
                UserId = currentUserId,
                GroupId = groupId,
                Message = "joined"
            });
        }

        public async Task LeaveGroupAsync()
        {
            await group!.RemoveAsync(Context);
            group.Except([Context.ContextId]).OnGroupUpdated(new ChatMessage
            {
                UserId = currentUserId,
                GroupId = currentGroupId,
                Message = "left"
            });
        }

        public async Task SendMessageAsync(ChatMessage chatMessage)
        {
            chatMessage.UserId = currentUserId;
            chatMessage.GroupId = currentGroupId;
            group!.Except([Context.ContextId]).OnMessageReceived(chatMessage);
        }
    }
}