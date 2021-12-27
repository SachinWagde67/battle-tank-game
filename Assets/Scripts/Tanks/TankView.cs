using UnityEngine;
using UnityEngine.UI;

namespace TankServices
{
    public class TankView : MonoBehaviour
    {
        private TankController tankController;
        private float movement, rotation;
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private float canFire;

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
            takeMoveInput();
            ShootBullet();    
        }

        private void FixedUpdate()
        {
            if(movement != 0)
            {
                tankController.Move(movement, tankController.tankModel.MovSpeed);
            }
            if (rotation != 0)
            {
                tankController.Rotate(rotation, tankController.tankModel.RotSpeed);
            }
            TankService.Instance.getPlayerPos(transform);
        }

        private void takeMoveInput()
        {
            movement = Input.GetAxis("Vertical");
            rotation = Input.GetAxis("Horizontal");
        }

        private void ShootBullet()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (canFire < Time.time)
                {
                    canFire = tankController.tankModel.FireRate + Time.time;
                    tankController.Shoot();
                }
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
            Destroy(this.gameObject);
        }
    }
}
