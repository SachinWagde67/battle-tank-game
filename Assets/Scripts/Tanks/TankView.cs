using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TankServices
{
    public class TankView : MonoBehaviour, IDamagable
    {
        private TankController tankController;
        private float movement, rotation, turretRotation;
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private float canFire;

        public GameObject healEffect;
        public GameObject turret;
        public Transform bulletShootPoint;
        public Slider healthSlider;
        public Image fillImage;

        private void Start()
        {
            tankController.setHealthUI();
            setTankColor();
        }

        public void SetTankController(TankController _tankController)
        {
            tankController = _tankController;
        }

        private void Update()
        {
            if (TankService.Instance.playOnPC)
            {
                takeMoveInput();
            }
            ShootBullet();    
        }

        private void FixedUpdate()
        {
            if(movement != 0 || tankController.tankMovementJoystick.Vertical != 0)
            {
                tankController.Move(movement, tankController.tankModel.MovSpeed);
            }
            if (rotation != 0 || tankController.tankMovementJoystick.Horizontal != 0)
            {
                tankController.Rotate(rotation, tankController.tankModel.RotSpeed);
            }
            if(turretRotation != 0 || tankController.turretRotateJoystick.Horizontal != 0)
            {
                tankController.TurretRotation(turretRotation, tankController.tankModel.turretRotSpeed);
            }
            TankService.Instance.getPlayerPos(transform);
        }

        private void takeMoveInput()
        {
            movement = Input.GetAxis("Vertical");
            rotation = Input.GetAxis("Horizontal");
            turretRotation = Input.GetAxisRaw("Horizontal1");
        }

        public void TakeDamage(float damage)
        {
            tankController.applyDamage(damage);
        }

        private void ShootBullet()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canFire < Time.time)
                {
                    canFire = tankController.tankModel.FireRate + Time.time;
                    tankController.Shoot();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<EnemyDropHealth>())
            {
                Destroy(other.gameObject);
                EnemyDropHealth.isDestroyed = true;
                tankController.enableHealEffect();
                tankController.increaseHealth();
            }
        }

        public void setTankColor()
        {
            MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = tankController.tankModel.TankColor;
            }
        }

        public void instantiateTankExplosionParticles()
        {
            ParticleSystem tankExplosion = Instantiate(explosionParticles, transform.position, transform.rotation);
            tankExplosion.Play();
            Destroy(tankExplosion, 1f);
        }

        public void destroyObject(GameObject _explosionPrefab)
        {
            Destroy(_explosionPrefab);
        }

        public void destroyView()
        {
            tankController = null;
            bulletShootPoint = null;
            explosionParticles = null;
            healEffect = null;
            Destroy(this.gameObject);
        }
    }
}
