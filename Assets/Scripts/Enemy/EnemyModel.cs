using BulletSO;
using EnemySO;
using UnityEngine;

namespace EnemyServices
{
    public class EnemyModel 
    {
        public float fireRate { get; private set; }
        public BulletScriptableObject bulletType { get; private set; }
        public Color enemyColor { get; private set; }
        
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
            bulletType = enemyScriptableObject.bulletType;
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
