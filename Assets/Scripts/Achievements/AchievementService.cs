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
        private AchievementController achievementController;

        private void Start()
        {
            CreateAchievement();
        }

        private void CreateAchievement()
        {
            AchievementModel model = new AchievementModel(bulletsFiredSO, enemiesKilledSO);
            achievementController = new AchievementController(model);
        }

        public AchievementController getAchievementController()
        {
            return achievementController;
        }
    }
}
