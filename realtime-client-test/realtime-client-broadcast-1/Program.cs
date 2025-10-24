using Grpc.Net.Client;
using MagicOnion.Client;
using Shared.IFs;

public class Program
{
    public static async Task Main(string[] args)
    {
        var address = args.Length > 0 ? "http://localhost:8080" : "http://localhost:5001";
        var p = new Program();
        await p.RunAsync(address);
    }
    public async Task RunAsync(string address)
    {
        var userId = Guid.NewGuid().ToString();
        Console.WriteLine("Your User ID: " + userId);
        using var channel = GrpcChannel.ForAddress(address);
        var receiver = new GroupChatHubReceiver();
        var client = await StreamingHubClient.ConnectAsync<IGroupChatHub, IGroupChatHubReceiver>(channel, receiver);
        await client.JoinGroupAsync(userId, "00000001");
        var inputLoop = Task.Run(async () =>
        {
            while (true)
            {
                var msg = Console.ReadLine();
                if (msg == null) break;
                if (msg.Equals("/quit", StringComparison.OrdinalIgnoreCase))
                {
                    await client.LeaveGroupAsync(userId);
                    break;
                }
                await client.SendMessageAsync(msg);
            }
        });
        await inputLoop;
        await client.DisposeAsync();
    }
}