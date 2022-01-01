using Achievements;
using EnemySO;
using Events;
using System.Collections;
using System.Collections.Generic;
using TankServices;
using UI;
using UnityEngine;

namespace EnemyServices
{
    public class EnemyService : SingletonGeneric<EnemyService>
    {
        public List<EnemyScriptableObject> enemyScriptableObject;
        public List<Transform> enemyPos;
        public List<EnemyController> enemies = new List<EnemyController>();
        public EnemyController enemyController;

        private int enemyCount;

        private void Start()
        {
            spawnRandomEnemy();
            SubscribeEvents();
        }

        private void spawnRandomEnemy()
        {
            enemyCount = Random.Range(3, enemyScriptableObject.Count + 1);

            for (int i = 0; i < enemyCount; i++)
            {
                int num = Random.Range(0, enemyPos.Count);
                int rand = Random.Range(0, enemyCount);
                CreateNewEnemy(enemyPos[num], rand);
            }
        }

        private EnemyController CreateNewEnemy(Transform trans, int rand)
        {
            EnemyView enemyView = enemyScriptableObject[rand].enemyView;
            Vector3 pos = trans.position;
            EnemyModel model = new EnemyModel(enemyScriptableObject[rand]);
            enemyController = new EnemyController(model, enemyView, pos);
            enemies.Add(enemyController);
            return enemyController;
        }

        private void SubscribeEvents()
        {
            EventService.Instance.OnEnemiesKilled += updateEnemyKilled;
            EventService.Instance.OnWavesSurvived += updateWavesSurvived;
        }

        private void updateEnemyKilled()
        {
            if (TankService.Instance.getTankController().tankModel != null)
            {
                TankService.Instance.getTankController().tankModel.enemiesKilled += 1;
                enemyCount--;
                AchievementService.Instance.getAchievementController().CheckForEnemiesKilledAchievement();
            }
        }

        private void updateWavesSurvived()
        {
            if (TankService.Instance.getTankController().tankModel != null)
            {
                TankService.Instance.getTankController().tankModel.wavesSurvived += 1;
                AchievementService.Instance.getAchievementController().CheckForWavesSurvivedAchievement();
            }
        }

        public void destroyEnemyTank(EnemyController enemyController)
        {
            enemyController.destroyEnemyController();
            
            if (enemyCount == 0)
            {
                RespawnEnemy();
            }
        }

        private async void RespawnEnemy()
        {
            await new WaitForSeconds(4f);
            UIManager.Instance.showWaves();
            EventService.Instance.invokeOnWavesSurvived();
            spawnRandomEnemy();
        }
    }
}
