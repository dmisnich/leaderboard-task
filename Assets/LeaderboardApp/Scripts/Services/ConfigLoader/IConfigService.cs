namespace LeaderboardApp.Scripts.Services.ConfigLoader
{
    public interface IConfigService
    {
        T Load<T>(string path);
    }
}