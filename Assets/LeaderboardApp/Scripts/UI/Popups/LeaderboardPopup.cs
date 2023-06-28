using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardApp.Scripts.Configs;
using LeaderboardApp.Scripts.Models;
using LeaderboardApp.Scripts.ScriptableObjects;
using SimplePopupManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;

namespace LeaderboardApp.Scripts.UI.Popups
{
    public class LeaderboardPopup : MonoBehaviour, IPopupInitialization
    {
        [Inject] private IPopupManagerService _popupManagerService;
        [Inject] private LeaderboardModel _leaderboardModel;
        [Inject] private LeaderboardUIData _leaderboardUIData;

        [SerializeField] private Button exitButton;
        [SerializeField] private Transform content;
        
        private LeaderboardPopupParams _leaderboardPopupParams;
        private LeaderboardData[] _params;
        private LeaderboardItem _cachedLeaderboardItem;
        private readonly List<LeaderboardItem> _leaderboardItems = new List<LeaderboardItem>();
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private void Start()
        {
            exitButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _cancellationToken.Cancel();
            _leaderboardPopupParams.PopupStateChanged(true);
            exitButton.onClick.RemoveListener(Hide);
            Addressables.ReleaseInstance(_cachedLeaderboardItem.gameObject);
        }

        private async void OnEnable()
        {
            _leaderboardPopupParams.PopupStateChanged(false);
            await SetAvatar(_cancellationToken);
        }

        public async Task Init(object param)
        {
            await SetPopup(param);
        }

        private async Task SetPopup(object param)
        {
            if (param == null) return;
            _leaderboardPopupParams = param as LeaderboardPopupParams;
            _params = _leaderboardModel.GetLeaderboardAllData();
            await SetPopup();
        }
        
        private async Task SetPopup()
        {
            if (_params != null)
            {
                int placeCount = 1;
                foreach (var item in _params)
                {
                    var result = await LoadItem();
                    result.Init(item, placeCount);
                    var data = _leaderboardUIData.GetData(item.type);
                    result.SetItemView(data);
                    placeCount++;
                }
            }
        }
        
        private async Task SetAvatar(CancellationTokenSource cancellationToken)
        {
            if (_params != null)
            {
                int itemCount = 0;
                foreach (var item in _params)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Debug.Log("Async function was canceled.");
                        return;
                    }
                    Sprite avatar = await _leaderboardModel.LoadAvatar(item.avatar);
                    _leaderboardItems[itemCount].InitAvatar(avatar);
                    itemCount++;
                }
            }
        }

        private async Task<LeaderboardItem> LoadItem()
        {
            if (TryLoadCached())
            {
                var leaderboardItemComponent = Instantiate(_cachedLeaderboardItem, content);
                _leaderboardItems.Add(leaderboardItemComponent);
                return leaderboardItemComponent;
            }

            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(nameof(LeaderboardItem), content);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject leaderboardItem = handle.Result;
                var leaderboardItemComponent = leaderboardItem.GetComponent<LeaderboardItem>();

                _cachedLeaderboardItem = leaderboardItemComponent;
                _leaderboardItems.Add(leaderboardItemComponent);
                return leaderboardItemComponent;
            }

            Debug.LogError($"Failed to load Popup with name {name}");
            return null;
        }

        private bool TryLoadCached()
        {
            return _cachedLeaderboardItem;
        }

        private void Hide()
        {
            _popupManagerService.ClosePopup(nameof(LeaderboardPopup));
        }
    }

    public class LeaderboardPopupParams
    {
        public event Action<bool> OnPopupStateChanged;

        public void PopupStateChanged(bool state)
        {
            OnPopupStateChanged?.Invoke(state);
        }
    }
}