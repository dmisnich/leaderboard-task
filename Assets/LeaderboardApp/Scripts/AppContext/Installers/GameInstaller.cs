using LeaderboardApp.Scripts.Models;
using LeaderboardApp.Scripts.Services.ConfigLoader;
using LeaderboardApp.Scripts.UI;
using SimplePopupManager;
using UnityEngine;
using Zenject;

namespace LeaderboardApp.Scripts.AppContext.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PopupCanvasProvider canvasProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<IPopupManagerService>().To<PopupManagerServiceExtention>().AsSingle();
            Container.Bind<PopupCanvasProvider>().FromInstance(canvasProvider).AsSingle();
            Container.Bind<IConfigService>().To<ConfigResourcesLoaderService>().AsSingle();
            Container.Bind<LeaderboardModel>().AsSingle();
        }
    }
}