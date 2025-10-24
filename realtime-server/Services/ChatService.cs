using MagicOnion;
using MagicOnion.Server;
using Shared.IFs;

namespace Services
{
    public class ChatService : ServiceBase<IChatService>, IChatService
    {
        public async UnaryResult<string> SendMessageAsync(string name, string message)
        {
            Console.WriteLine($"[{name}] {message}");
            return await new UnaryResult<string>($"Received: {message}");
        }
    }
}
