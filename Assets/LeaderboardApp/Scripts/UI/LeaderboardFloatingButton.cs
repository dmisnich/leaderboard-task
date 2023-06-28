using LeaderboardApp.Scripts.UI.Popups;
using SimplePopupManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LeaderboardApp.Scripts.UI
{
    public class LeaderboardFloatingButton : MonoBehaviour
    {
        [Inject] private IPopupManagerService _popupManagerService;

        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private GameObject _leaderboardObject;
        
        private readonly LeaderboardPopupParams _leaderboardPopupParams = new LeaderboardPopupParams();

        private void Start()
        {
            _leaderboardButton.onClick.AddListener(ShowLeaderboard);
            _leaderboardPopupParams.OnPopupStateChanged += PopupStateChanged;
        }

        private void OnDestroy()
        {
            _leaderboardButton.onClick.RemoveListener(ShowLeaderboard);
            _leaderboardPopupParams.OnPopupStateChanged -= PopupStateChanged;
        }

        private void ShowLeaderboard()
        {
            _popupManagerService.OpenPopup(nameof(LeaderboardPopup), _leaderboardPopupParams);
        }

        private void PopupStateChanged(bool state)
        {
            _leaderboardObject.SetActive(state);
        }
    }
}