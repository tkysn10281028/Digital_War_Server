namespace Shared.ApiEndPoints
{
    public class S3GetMapNameList : EndPointBase<S3GetMapNameList.Request, S3GetMapNameList.Response>
    {
        public override string Path => "S3/getMapNameList";
        public class Request : RequestBase
        {

        }

        public class Response : ResponseBase
        {
            public List<string> MapNameList { get; set; } = new();
        }
    }
}