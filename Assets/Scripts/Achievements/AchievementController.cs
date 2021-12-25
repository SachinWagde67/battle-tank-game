using TankServices;
using UI;

namespace Achievements
{
    public class AchievementController 
    {
        public AchievementModel achievementModel { get; private set; }

        private int currentBulletsFiredAchievementLevel;
        private int currentEnemiesKilledAchievementLevel;

        public AchievementController(AchievementModel model)
        {
            currentBulletsFiredAchievementLevel = 0;
            currentEnemiesKilledAchievementLevel = 0;
            achievementModel = model;
        }

        public void CheckForBulletsFiredAchievement()
        {
            for (int i = 0; i < achievementModel.bulletsFiredAchievementSO.achievements.Length; i++)
            {
                if(i != currentBulletsFiredAchievementLevel)
                {
                    continue;
                }
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
            for (int i = 0; i < achievementModel.enemiesKilledAchievementSO.achievements.Length; i++)
            {
                if (i != currentEnemiesKilledAchievementLevel)
                {
                    continue;
                }
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

        private void unlockAchievement(string achievement, string achievementInfo)
        {
            UIManager.Instance.ShowAchievement(achievement, achievementInfo);
        }
    }
}
