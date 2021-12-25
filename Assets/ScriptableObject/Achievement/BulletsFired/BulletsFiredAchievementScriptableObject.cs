using System;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObject/Achievement/BulletsFired/NewBulletsFiredAchievement")]
    public class BulletsFiredAchievementScriptableObject : ScriptableObject
    {
        public BulletsFiredAchievements[] achievements;

        [Serializable]
        public class BulletsFiredAchievements
        {
            public enum BulletsFiredAchievementType
            {
                None,
                SharpShooter,
                Commando,
                WeaponMaster
            }

            public string achievementName;
            public string achievementInfo;
            public BulletsFiredAchievementType bulletsFiredAchievementType;
            public int requirement;
        }
    }

}
