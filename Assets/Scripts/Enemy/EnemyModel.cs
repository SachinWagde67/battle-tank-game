using BulletSO;
using EnemySO;
using UnityEngine;

namespace EnemyServices
{
    public class EnemyModel 
    {
        public float fireRate { get; private set; }
        public float followRadius { get; private set; }
        public float patrolTime { get; private set; }
        public BulletScriptableObject bulletType { get; private set; }
        public Color enemyColor { get; private set; }
        
        public bool canDropHealth { get; set; }
        public float health { get; set; }
        public float maxHealth { get; }
        public Color fullHealthColor { get; }
        public Color zeroHealthColor { get; }

        private EnemyController enemyController;

        public EnemyModel(EnemyScriptableObject enemyScriptableObject)
        {
            maxHealth = enemyScriptableObject.health;
            health = enemyScriptableObject.health;
            enemyColor = enemyScriptableObject.enemyColor;
            fireRate = enemyScriptableObject.fireRate;
            followRadius = enemyScriptableObject.followRadius;
            patrolTime = enemyScriptableObject.patrolTime;
            bulletType = enemyScriptableObject.bulletType;
            canDropHealth = enemyScriptableObject.canDropHealth;
            fullHealthColor = Color.green;
            zeroHealthColor = Color.red;
        }

        public void setEnemyController(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }

        public void destroyModel()
        {
            bulletType = null;
            enemyController = null;
        }
    }
}
