using TankServices;
using UI;

namespace Achievements
{
    public class AchievementController 
    {
        public AchievementModel achievementModel { get; private set; }

        private int currentBulletsFiredAchievementLevel;
        private int currentEnemiesKilledAchievementLevel;
        private int currentWavesSurvivedAchievementLevel;

        public AchievementController(AchievementModel model)
        {
            currentBulletsFiredAchievementLevel = 0;
            currentEnemiesKilledAchievementLevel = 0;
            currentWavesSurvivedAchievementLevel = 0;
            achievementModel = model;
        }

        public void CheckForBulletsFiredAchievement()
        {
            for (int i = 0; i < achievementModel.bulletsFiredAchievementSO.achievements.Length;)
            {
                if(TankService.Instance.getTankController().tankModel.bulletsFired == achievementModel.bulletsFiredAchievementSO.achievements[i].requirement)
                {
                    string achievement = achievementModel.bulletsFiredAchievementSO.achievements[i].achievementName;
                    string achievementInfo = achievementModel.bulletsFiredAchievementSO.achievements[i].achievementInfo;
                    unlockAchievement(achievement, achievementInfo);
                    currentBulletsFiredAchievementLevel = i + 1;
                }
                break;
            }
        }

        public void CheckForEnemiesKilledAchievement()
        {
            for (int i = 0; i < achievementModel.enemiesKilledAchievementSO.achievements.Length;)
            {
                if (TankService.Instance.getTankController().tankModel.enemiesKilled == achievementModel.enemiesKilledAchievementSO.achievements[i].requirement)
                {
                    string achievement = achievementModel.enemiesKilledAchievementSO.achievements[i].achievementName;
                    string achievementInfo = achievementModel.enemiesKilledAchievementSO.achievements[i].achievementInfo;
                    unlockAchievement(achievement, achievementInfo);
                    currentEnemiesKilledAchievementLevel = i + 1;
                }
                break;
            }
        }

        public void CheckForWavesSurvivedAchievement()
        {
            for (int i = 0; i < achievementModel.wavesSurvivedAchievementSO.achievements.Length;)
            {
                if (TankService.Instance.getTankController().tankModel.wavesSurvived == achievementModel.wavesSurvivedAchievementSO.achievements[i].requirement)
                {
                    string achievement = achievementModel.wavesSurvivedAchievementSO.achievements[i].achievementName;
                    string achievementInfo = achievementModel.wavesSurvivedAchievementSO.achievements[i].achievementInfo;
                    unlockAchievement(achievement, achievementInfo);
                    currentWavesSurvivedAchievementLevel = i + 1;
                }
                break;
            }
        }

        private void unlockAchievement(string achievement, string achievementInfo)
        {
            UIManager.Instance.showAchievement(achievement, achievementInfo);
        }
    }
}
