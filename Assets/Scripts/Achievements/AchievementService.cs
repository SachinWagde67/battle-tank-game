using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    public class AchievementService : SingletonGeneric<AchievementService>
    {
        public BulletsFiredAchievementScriptableObject bulletsFiredSO;
        public EnemiesKilledAchievementScriptableObject enemiesKilledSO;
        public WavesSurvivedAchievementScriptableObject wavesSurvivedSO;

        private AchievementController achievementController;

        private void Start()
        {
            CreateAchievement();
        }

        private void CreateAchievement()
        {
            AchievementModel model = new AchievementModel(bulletsFiredSO, enemiesKilledSO, wavesSurvivedSO);
            achievementController = new AchievementController(model);
        }

        public AchievementController getAchievementController()
        {
            return achievementController;
        }
    }
}
