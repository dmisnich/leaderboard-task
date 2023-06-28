using System;
using System.Linq;
using LeaderboardApp.Scripts.Enums;
using UnityEngine;

namespace LeaderboardApp.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LeaderboardUIData", menuName = "Create LeaderboardUIData")]
    public class LeaderboardUIData : ScriptableObject
    {
        [SerializeField] private LeaderboardItemData[] data;

        public LeaderboardItemData GetData(eLeaderboardItemType itemType)
        {
            var itemData = data.First(x => x.LeaderboardItemType == itemType);
            return itemData;
        }
    }

    [Serializable]
    public struct LeaderboardItemData
    {
        public eLeaderboardItemType LeaderboardItemType => leaderboardItemType;
        public Vector2 ItemScale => itemScale;
        public Color ItemColor => itemColor;
        
        [SerializeField] private eLeaderboardItemType leaderboardItemType;
        [SerializeField] private Vector2 itemScale;
        [SerializeField] private Color itemColor;
    }
}