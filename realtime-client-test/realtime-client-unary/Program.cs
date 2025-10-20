using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;
using Shared.IFs;

class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5001");

        // IChatService
        var response = await PrepareMagicOnionClient<IChatService>(channel).SendMessageAsync("Alice", "message");
        Console.WriteLine($"Executed IChatService. Response : {response}");
    }

    private static T PrepareMagicOnionClient<T>(GrpcChannel channel) where T : IService<T>
    {
        return MagicOnionClient.Create<T>(channel);
    }
}
