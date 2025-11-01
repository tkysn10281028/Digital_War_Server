using Amazon.S3;
using Amazon.S3.Model;
using ApiServer.Project.Common;

namespace ApiServer.Project.Externals
{
    public class ApiServerAmazonS3Client : IInjectable
    {
        private readonly AmazonS3Client _s3;
        private readonly string _bucketName;

        public ApiServerAmazonS3Client(IConfiguration config)
        {
            var serviceUrl = config["S3:ServiceUrl"];
            var accessKey = config["S3:AccessKey"];
            var secretKey = config["S3:SecretKey"];
            _bucketName = config["S3:BucketName"] ?? "";

            var s3Config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true // MinIO互換
            };
            _s3 = new AmazonS3Client(accessKey, secretKey, s3Config);
        }
        public async Task<List<string>> GetFileNameList(string? fileName = null)
        {
            var files = new List<string>();
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName
            };

            ListObjectsV2Response response;
            do
            {
                response = await _s3.ListObjectsV2Async(request);

                foreach (var obj in response.S3Objects)
                {
                    if (string.IsNullOrEmpty(fileName) || obj.Key.Contains(fileName))
                    {
                        files.Add(obj.Key);
                    }
                }
                request.ContinuationToken = response.NextContinuationToken;
            }
            while (response.IsTruncated == true);
            return files;
        }
    }
}