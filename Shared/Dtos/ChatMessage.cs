using MessagePack;

namespace Shared.Dtos
{
    [MessagePackObject]
    public class ChatMessage
    {
        [Key(0)]
        public string GroupId { get; set; } = "";

        [Key(1)]
        public string UserId { get; set; } = "";

        [Key(2)]
        public string Message { get; set; } = "";
    }
}
