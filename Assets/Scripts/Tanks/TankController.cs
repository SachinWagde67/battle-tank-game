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

        public void applyDamage(float damage)
        {
            tankModel.Health -= damage;
            setHealthUI();
            if(tankModel.Health <= 0)
            {
                tankView.instantiateTankExplosionParticles();
                AudioManager.Instance.explosionAudio.GetComponent<AudioSource>().Play();
                destroyController();
                GameManager.Instance.DestroyAllObjects();
            }
        }

        public void setHealthUI()
        {
            tankView.healthSlider.maxValue = tankModel.MaxHealth;
            tankView.healthSlider.value = tankModel.Health;
            tankView.fillImage.color = Color.Lerp(tankModel.ZeroHealthColor, tankModel.FullHealthColor, tankModel.Health / tankModel.MaxHealth);
        }

        public void Move(float moveAxis, float movSpeed)
        {
            Vector3 move = tankView.transform.forward * moveAxis * movSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + move);
        }

        public void Rotate(float rotateAxis, float rotateSpeed)
        {
            float rotate = rotateAxis * rotateSpeed * Time.deltaTime;
            Quaternion turn = Quaternion.Euler(0f, rotate, 0f);
            rb.MoveRotation(rb.rotation * turn);
        }

        public void Shoot()
        {
            AudioManager.Instance.shootingAudio.GetComponent<AudioSource>().Play();
            BulletService.Instance.CreateBullet(tankView.bulletShootPoint.position, tankView.transform.rotation, tankModel.BulletType);
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
        }
    }
}
