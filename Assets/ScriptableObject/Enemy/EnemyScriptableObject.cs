using BulletSO;
using UnityEngine;
using EnemyServices;
using TankServices;

namespace EnemySO
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy/NewEnemy")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public EnemyView enemyView;
        public Color enemyColor;
        public float health;
        public float fireRate;
        public float followRadius;
        public float patrolTime;
        public BulletScriptableObject bulletType;
        public bool canDropHealth;
    }
}
