using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using ApiServer.Project.Common;
using ApiServer.Project.Database;
using ApiServer.Project.Domains;
using ApiServer.Project.Domains.Logics;
using ApiServer.Project.Externals;
using Shared.ApiEndPoints;

namespace ApiServer.Project.Services
{
    public class S3Service : IInjectable
    {
        private ApiServerAmazonS3Client _s3Client;
        private UserMapLogic _userMapLogic;

        public S3Service(ApiServerAmazonS3Client s3Client, UserMapLogic userMapLogic)
        {
            _s3Client = s3Client;
            _userMapLogic = userMapLogic;
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

        public async Task<List<string>> GetMapNameListAsync(S3GetMapNameList.Request request)
        {
            var userMapList = await _userMapLogic.FindByGuildId(request.GuildId);
            if (userMapList.Count == 0)
            {
                var allMapNameList = await _s3Client.GetFileNameList();
                var randomMapNameList = GetRandomMapNameList(allMapNameList);
                await _userMapLogic.Insert(request.GuildId, randomMapNameList);
                return randomMapNameList;
            }
            return userMapList.Select(u => u.MapName).ToList();
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
