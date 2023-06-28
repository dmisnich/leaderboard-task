using System.Threading.Tasks;
using LeaderboardApp.Scripts.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SimplePopupManager
{
    public class PopupManagerServiceExtention : PopupManagerServiceService
    {
        [Inject] private PopupCanvasProvider _canvasProvider;
        [Inject] private DiContainer _container;

        protected override async Task LoadPopup(string name, object param)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(name, _canvasProvider.GetTransform());
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject popupObject = handle.Result;

                popupObject.SetActive(false);
                _container.InjectGameObject(popupObject);
                IPopupInitialization[] popupInitComponents = popupObject.GetComponents<IPopupInitialization>();

                foreach (IPopupInitialization component in popupInitComponents)
                {
                    await component.Init(param);
                }

                popupObject.SetActive(true);
                m_Popups.Add(name, popupObject);
            }
            else
            {
                Debug.LogError($"Failed to load Popup with name {name}");
            }
        }
    }
}