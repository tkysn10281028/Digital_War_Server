
using MagicOnion;

namespace Shared.IFs
{
    public interface IChatService : IService<IChatService>
    {
        UnaryResult<string> SendMessageAsync(string name, string message);
    }
}
