using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using ApiServer.Project.Common;
using ApiServer.Project.Database;
using ApiServer.Project.Domains;
using ApiServer.Project.Domains.Logics;
using ApiServer.Project.Externals;

namespace ApiServer.Project.Services
{
    public class S3Service : IInjectable
    {
        private ApiServerAmazonS3Client _s3Client;
        private UserGuildLogic _userGuildLogic;

        public S3Service(ApiServerAmazonS3Client s3Client, UserGuildLogic userGuildLogic)
        {
            _s3Client = s3Client;
            _userGuildLogic = userGuildLogic;
        }

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

        public async Task<List<string>> GetMapNameListAsync(long userId, long groupId)
        {
            var allMapNameList = await _s3Client.GetFileNameList();
            return GetRandomMapNameList(allMapNameList);
        }

        private List<string> GetRandomMapNameList(List<string> list)
        {
            var output = new List<string>();
            foreach (var word in _targetWordList)
            {
                var fileNames = list.Where(r => r.Contains(word)).ToList();
                if (fileNames.Count != 0)
                {
                    output.Add(fileNames[new Random().Next(fileNames.Count)]);
                }
            }
            return output;
        }
    }
}
