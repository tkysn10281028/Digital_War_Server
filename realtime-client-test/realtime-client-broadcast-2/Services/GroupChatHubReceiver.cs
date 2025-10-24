using Shared.IFs;

public class GroupChatHubReceiver : IGroupChatHubReceiver
{
    public void OnGroupUpdated(string groupId, string userId, string message)
    {
        Console.WriteLine($"User: {userId} {message} the Group: {groupId} !");
    }

    public void OnMessageReceived(string groupId, string userId, string message)
    {
        Console.WriteLine($"User: {userId} Sent a message : ' {message} ' to the Group: {groupId} !");
    }
}