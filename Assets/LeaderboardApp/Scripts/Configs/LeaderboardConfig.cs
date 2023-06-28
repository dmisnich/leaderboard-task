using System;
using LeaderboardApp.Scripts.Enums;

namespace LeaderboardApp.Scripts.Configs
{
    [Serializable]
    public class LeaderboardConfig
    {
        public LeaderboardData[] leaderboard;
    }

    [Serializable]
    public struct LeaderboardData
    {
        public string name;
        public int score;
        public string avatar;
        public eLeaderboardItemType type;
    }
}