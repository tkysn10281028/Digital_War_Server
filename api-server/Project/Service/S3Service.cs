using Amazon.S3;
using Amazon.S3.Model;

namespace ApiServer.Project.Services
{
    public class S3Service
    {
        private readonly AmazonS3Client _s3;
        private readonly string _bucketName;
        private readonly List<string> _targetWordList =
        [
            "map_down_border",
            "map_left_border",
            "map_leftdown_border",
            "map_leftup_border",
            "map_noborder",
            "map_right_border",
            "map_rightdown_border",
            "map_rightup_border",
            "map_up_border",
        ];

        public S3Service(IConfiguration config)
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

        public async Task<List<string>> GetFileListAsync()
        {
            var result = await GetAllFileListAsync();
            var output = new List<string>();
            foreach (var word in _targetWordList)
            {
                var fileNames = result.Where(r => r.Contains(word)).ToList();
                output.Add(fileNames[new Random().Next(fileNames.Count)]);
            }
            return output;
        }
        private async Task<List<string>> GetAllFileListAsync()
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
                    files.Add(obj.Key);
                }

                request.ContinuationToken = response.NextContinuationToken;
            }
            while (response.IsTruncated == true);

            return files;
        }
    }
}
