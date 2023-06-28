using LeaderboardApp.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace LeaderboardApp.Scripts.AppContext.Installers
{
    [CreateAssetMenu(fileName = "MyScriptableObjectInstaller", menuName = "Installers/MyScriptableObjectInstaller")]
    public class DataInstaller : ScriptableObjectInstaller
    {
        public LeaderboardUIData leaderboardUIData;

        public override void InstallBindings()
        {
            Container.BindInstance(leaderboardUIData).AsSingle();
        }
    }
}