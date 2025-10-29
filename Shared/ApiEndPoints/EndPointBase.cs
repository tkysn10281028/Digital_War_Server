namespace Shared.ApiEndPoints
{
    public abstract class EndPointBase<TRequest, TResponse>
        where TRequest : class, new()
        where TResponse : class, new()
    {
        public abstract string Path { get; }

        public virtual TRequest CreateRequest() => new();

        public virtual TResponse CreateResponse() => new();
    }
}