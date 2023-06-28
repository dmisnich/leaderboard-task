using LeaderboardApp.Scripts.Configs;
using LeaderboardApp.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeaderboardApp.Scripts.UI.Popups
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image container;
        [SerializeField] private RectTransform containerTransform;
        [SerializeField] private GameObject iconLoadingText;
        [SerializeField] private TMP_Text place;
        [SerializeField] private TMP_Text name;
        [SerializeField] private TMP_Text score;
        [SerializeField] private TMP_Text type;

        public void Init(LeaderboardData data, int placeValue)
        {
            place.text = placeValue.ToString();
            name.text = data.name;
            score.text = data.score.ToString();
            type.text = data.type.ToString();
        }
        
        public void InitAvatar(Sprite avatar)
        {
            if (!icon) return;
            icon.sprite = avatar;
            iconLoadingText.SetActive(false);
        }

        public void SetItemView(LeaderboardItemData data)
        {
            containerTransform.sizeDelta = data.ItemScale;
            container.color = data.ItemColor;
        }
    }
}