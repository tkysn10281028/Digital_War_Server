namespace Shared.ApiEndPoints
{
    public class RequestBase
    {
        public long UserId { get; set; }
        public long GuildId { get; set; }
    }

    public class ResponseBase
    {

    }

    public abstract class EndPointBase<TRequest, TResponse>
        where TRequest : RequestBase, new()
        where TResponse : ResponseBase, new()
    {
        public abstract string Path { get; }

        public virtual TRequest CreateRequest() => new();

        public virtual TResponse CreateResponse() => new();
    }
}