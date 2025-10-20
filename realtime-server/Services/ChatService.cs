using MagicOnion;
using MagicOnion.Server;
using Shared.IFs;

public class ChatService : ServiceBase<IChatService>, IChatService
{
    public async UnaryResult<string> SendMessageAsync(string name, string message)
    {
        Console.WriteLine($"[{name}] {message}");
        return await new UnaryResult<string>($"Received: {message}");
    }
}
