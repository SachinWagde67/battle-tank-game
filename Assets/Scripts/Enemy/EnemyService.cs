using Achievements;
using EnemySO;
using Events;
using System.Collections.Generic;
using TankServices;
using UnityEngine;

namespace EnemyServices
{
    public class EnemyService : SingletonGeneric<EnemyService>
    {
        public List<EnemyScriptableObject> enemyScriptableObject;
        public List<Transform> enemyPos;
        public List<EnemyController> enemies = new List<EnemyController>();
        public EnemyController enemyController;
        private int count;
        private int enemyCount;

        private void Start()
        {
            enemyCount = enemyScriptableObject.Count;
            for (int i = 0; i < enemyCount; i++)
            {
                count = enemyPos.Count;
                int num = Random.Range(0, count);
                int rand = Random.Range(0, enemyCount);
                CreateNewEnemy(enemyPos[num], rand);
                enemyPos.RemoveAt(num);
            }
            SubscribeEvents();
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
        }

        private void updateEnemyKilled()
        {
            if (TankService.Instance.getTankController().tankModel != null)
            {
                TankService.Instance.getTankController().tankModel.enemiesKilled += 1;
                AchievementService.Instance.getAchievementController().CheckForEnemiesKilledAchievement();
            }
        }

        public void destroyEnemyTank(EnemyController enemyController)
        {
            enemyController.destroyEnemyController();
        }

    }
}
