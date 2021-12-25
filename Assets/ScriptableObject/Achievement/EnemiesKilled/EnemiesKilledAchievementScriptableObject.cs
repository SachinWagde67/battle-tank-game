using System;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "EnemiesKilledAchievementSO", menuName = "ScriptableObject/Achievement/EnemiesKilled/NewEnemiesKilledAchievement")]
    public class EnemiesKilledAchievementScriptableObject : ScriptableObject
    {
        public EnemyKilledAchievements[] achievements;

        [Serializable]
        public class EnemyKilledAchievements
        {
            public enum EnemyKilledAchievementType
            {
                None,
                KillingSpree,
                Rampage,
                Unstoppable
            }

            public string achievementName;
            public string achievementInfo;
            public EnemyKilledAchievementType enemyKilledAchievementType;
            public int requirement;
        }
    }   
}
