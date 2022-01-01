using System;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "WavesSurvivedAchievementSO", menuName = "ScriptableObject/Achievement/Waves/NewWavesSurvivedAchievement")]
    public class WavesSurvivedAchievementScriptableObject : ScriptableObject
    {
        public WavesSurvivedAchievements[] achievements;

        [Serializable]
        public class WavesSurvivedAchievements
        {
            public enum WavesSurvivedAchievementType
            {
                None,
                LoneSurvivor,
                StayingAlive,
                HeroNeverDie
            }

            public string achievementName;
            public string achievementInfo;
            public WavesSurvivedAchievementType wavesSurvivedAchievementType;
            public int requirement;
        }
    }
}
