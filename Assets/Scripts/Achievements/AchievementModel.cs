﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    public class AchievementModel
    {
        public BulletsFiredAchievementScriptableObject bulletsFiredAchievementSO { get; private set; }
        public EnemiesKilledAchievementScriptableObject enemiesKilledAchievementSO { get; private set; }
        public WavesSurvivedAchievementScriptableObject wavesSurvivedAchievementSO { get; private set; }

        public AchievementModel(BulletsFiredAchievementScriptableObject bulletsFired, EnemiesKilledAchievementScriptableObject enemiesKilled, WavesSurvivedAchievementScriptableObject wavesSurvived)
        {
            bulletsFiredAchievementSO = bulletsFired;
            enemiesKilledAchievementSO = enemiesKilled;
            wavesSurvivedAchievementSO = wavesSurvived;
        }
    }
}
