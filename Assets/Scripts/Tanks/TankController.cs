using BulletServices;
using UnityEngine;
using GamePlayServices;
using AudioServices;
using Events;
using Achievements;

namespace TankServices
{
    public class TankController
    {
        private Rigidbody rb;
        private Camera camera;
        
        [HideInInspector] public Joystick tankMovementJoystick;
        [HideInInspector] public Joystick turretRotateJoystick;

        public TankModel tankModel { get; set; }
        public TankView tankView { get; set; }

        public TankController(TankModel _model, TankView _tankView)
        {
            tankModel = _model;
            tankView = GameObject.Instantiate<TankView>(_tankView);
            rb = tankView.GetComponent<Rigidbody>();
            tankView.SetTankController(this);
            tankModel.SetTankController(this);
            SubscribeEvents();
        }

        public void setCameraReference(Camera _cam)
        {
            camera = _cam;
            camera.transform.SetParent(tankView.transform);
        }

        public void setJoysticks(Joystick movementJoystick, Joystick turretJoystick)
        {
            tankMovementJoystick = movementJoystick;
            turretRotateJoystick = turretJoystick;
        }

        public void applyDamage(float damage)
        {
            tankModel.Health -= damage;
            setHealthUI();
            if(tankModel.Health <= 0)
            {
                tankView.instantiateTankExplosionParticles();
                AudioManager.Instance.explosionAudio.GetComponent<AudioSource>().Play();
                destroyController();
            }
        }

        public void setHealthUI()
        {
            tankView.healthSlider.maxValue = tankModel.MaxHealth;
            tankView.healthSlider.value = tankModel.Health;
            tankView.fillImage.color = Color.Lerp(tankModel.ZeroHealthColor, tankModel.FullHealthColor, tankModel.Health / tankModel.MaxHealth);
        }

        public void increaseHealth()
        {
            if (tankModel.Health <= tankModel.MaxHealth)
            {
                tankModel.Health += Random.Range(10, 26);
                setHealthUI();
            }
        }

        public async void enableHealEffect()
        {
            tankView.healEffect.SetActive(true);
            await new WaitForSeconds(2f);
            tankView.healEffect.SetActive(false);
        }

        public void Move(float moveAxis, float movSpeed)
        {
            Vector3 move = tankMovementJoystick.Vertical * rb.transform.forward * tankModel.MovSpeed * Time.deltaTime;
            rb.MovePosition(rb.transform.position + move);

            if (TankService.Instance.playOnPC)
            {
                Vector3 tankMove = tankView.transform.forward * moveAxis * movSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + tankMove);
            }
        }

        public void Rotate(float rotateAxis, float rotateSpeed)
        {
            Quaternion rotate =  Quaternion.Euler(Vector3.up * tankMovementJoystick.Horizontal * tankModel.RotSpeed * Time.deltaTime);
            rb.MoveRotation(rb.transform.rotation * rotate);

            if (TankService.Instance.playOnPC)
            {
                float rot = rotateAxis * rotateSpeed * Time.deltaTime;
                Quaternion turn = Quaternion.Euler(0f, rot, 0f);
                rb.MoveRotation(rb.rotation * turn);
            }
        }

        public void TurretRotation(float rotateAxis, float turretRotSpeed)
        {
            Vector3 rotate = Vector3.up * turretRotateJoystick.Horizontal * tankModel.turretRotSpeed * Time.deltaTime;
            tankView.turret.transform.Rotate(rotate, Space.Self);

            if(TankService.Instance.playOnPC)
            {
                Vector3 rot = Vector3.up * rotateAxis * turretRotSpeed * Time.deltaTime;
                tankView.turret.transform.Rotate(rot, Space.Self);
            }
        }

        public void Shoot()
        {
            AudioManager.Instance.shootingAudio.GetComponent<AudioSource>().Play();
            BulletService.Instance.CreateBullet(tankView.bulletShootPoint.position, tankView.turret.transform.rotation, tankModel.BulletType);
            EventService.Instance.invokeOnBulletsFired();
        }

        private void SubscribeEvents()
        {
            EventService.Instance.OnBulletsFired += updateBulletsFired;
        }

        private void updateBulletsFired()
        {
            tankModel.bulletsFired += 1;
            AchievementService.Instance.getAchievementController().CheckForBulletsFiredAchievement();
        }

        private void UnsubscribeEvents()
        {
            EventService.Instance.OnBulletsFired -= updateBulletsFired;
        }

        public void destroyController()
        {
            camera.transform.parent = null;
            tankModel.destroyModel();
            tankView.destroyView();
            tankModel = null;
            tankView = null;
            UnsubscribeEvents();
            GameManager.Instance.DestroyAllObjects();
        }
    }
}
