using System.Threading.Tasks;
using LeaderboardApp.Scripts.Configs;
using LeaderboardApp.Scripts.Services.ConfigLoader;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace LeaderboardApp.Scripts.Models
{
    public class LeaderboardModel
    {
        [Inject] private IConfigService _configService;

        private LeaderboardConfig _leaderboardConfig;

        [Inject]
        private void Init()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            _leaderboardConfig = _configService.Load<LeaderboardConfig>("Leaderboard");
        }

        public LeaderboardData[] GetLeaderboardAllData()
        {
            return _leaderboardConfig.leaderboard;
        }

        public async Task<Sprite> LoadAvatar(string path)
        {
            var texture = await DownloadImageAsync(path);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            return sprite;
        }
        
        private async Task<Texture2D> DownloadImageAsync(string imageUrl)
        {
            var request = UnityWebRequestTexture.GetTexture(imageUrl);
            
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Delay(100);
            }

            return request.result == UnityWebRequest.Result.Success
                ? DownloadHandlerTexture.GetContent(request)
                : null;
        }
    }
}