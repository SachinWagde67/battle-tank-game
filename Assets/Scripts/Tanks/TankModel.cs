using BulletSO;
using UnityEngine;
using TankSO;
using Achievements;

namespace TankServices
{
    public class TankModel
    {
        private TankController tankController;

        public float RotSpeed { get; private set; }
        public float turretRotSpeed { get; private set; }
        public float MovSpeed { get; private set; }
        public float FireRate { get; private set; }
        public Color TankColor { get; private set; }
        public TankType TankType { get; private set; }
        public BulletScriptableObject BulletType { get; private set; }
        
        public float Health { get; set; }
        public int bulletsFired { get; set; }
        public int enemiesKilled { get; set; }
        public int wavesSurvived { get; set; }

        public float MaxHealth { get; }
        public Color FullHealthColor { get; }
        public Color ZeroHealthColor { get; }

        public TankModel(TankScriptableObjects tankScriptableObjects)
        {
            TankType = tankScriptableObjects.tankType;
            MovSpeed = tankScriptableObjects.movSpeed;
            RotSpeed = tankScriptableObjects.rotSpeed;
            turretRotSpeed = tankScriptableObjects.turretRotSpeed;
            Health = tankScriptableObjects.health;
            MaxHealth = tankScriptableObjects.health;
            BulletType = tankScriptableObjects.bulletType;
            FireRate = tankScriptableObjects.fireRate;
            TankColor = tankScriptableObjects.tankColor;
            FullHealthColor = Color.green;
            ZeroHealthColor = Color.red;
            bulletsFired = 0;
            enemiesKilled = 0;
            wavesSurvived = 0;
        }

        public void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }

        public void destroyModel()
        {
            BulletType = null;
            tankController = null;
        }
    }
}
