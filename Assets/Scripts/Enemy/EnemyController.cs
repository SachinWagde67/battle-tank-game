using AudioServices;
using BulletServices;
using Events;
using UnityEngine;

namespace EnemyServices
{
    public class EnemyController
    {
        public EnemyModel enemyModel { get; set; }
        public EnemyView enemyView { get; set; }

        public EnemyController(EnemyModel model, EnemyView view, Vector3 pos)
        {
            enemyModel = model;
            enemyView = GameObject.Instantiate<EnemyView>(view, pos, Quaternion.identity);
            enemyView.setEnemyController(this);
            enemyModel.setEnemyController(this);
        }

        public void Move()
        { 
            if(enemyView.playerTransform != null)
            {
                float distance = Vector3.Distance(enemyView.transform.position, enemyView.playerTransform.position);
                if(distance <= enemyModel.followRadius)
                {
                    Follow();
                }
                else
                {
                    Patrol();
                }
            }
        }

        public void Follow()
        {
            enemyView.transform.LookAt(enemyView.playerTransform);
            enemyView.agent.SetDestination(enemyView.playerTransform.position);
            shootBullet();
        }

        private void shootBullet()
        {
            if(enemyView.canFire < Time.time)
            {
                enemyView.canFire = enemyModel.fireRate + Time.time;
                BulletService.Instance.CreateBullet(enemyView.shootPoint.position, enemyView.transform.rotation, enemyModel.bulletType);
            }
        }

        public void Patrol()
        {
            enemyView.timer += Time.deltaTime;
            if(enemyView.timer > enemyModel.patrolTime)
            {
                Vector3 newDestination = GetRandomPos();
                enemyView.agent.SetDestination(newDestination);
                enemyView.timer = 0f;
            }
        }

        private Vector3 GetRandomPos()
        {
            float x = Random.Range(enemyView.minX, enemyView.maxX);
            float z = Random.Range(enemyView.minZ, enemyView.maxZ);
            Vector3 randDir = new Vector3(x, 0f, z);
            return randDir;
        }

        public void applyDamage(float damage)
        {
            enemyModel.health -= damage;
            setHealthUI();
            if(enemyModel.health <= 0)
            {
                enemyView.instantiateTankExplosionParticles();
                AudioManager.Instance.explosionAudio.GetComponent<AudioSource>().Play();
                if(enemyModel.canDropHealth)
                {
                    destroyDropHealth();
                }
                enemyDead();
            }
        }

        private async void destroyDropHealth()
        {
            GameObject healthObj =  enemyView.instantiateDropHealth();
            await new WaitForSeconds(5f);

            if (!EnemyDropHealth.isDestroyed)
            {
                EnemyDropHealth.destroyDropHealth(healthObj);
            }
        }

        public void setHealthUI()
        {
            enemyView.healthSlider.maxValue = enemyModel.maxHealth;
            enemyView.healthSlider.value = enemyModel.health;
            enemyView.fillImage.color = Color.Lerp(enemyModel.zeroHealthColor, enemyModel.fullHealthColor, enemyModel.health / enemyModel.maxHealth);
        }

        public void enemyDead()
        {
            EventService.Instance.invokeOnEnemiesKilled();
            EnemyService.Instance.destroyEnemyTank(this);
        }

        public void destroyEnemyController()
        {
            enemyModel.destroyModel();
            enemyView.destroyView();
            enemyModel = null;
            enemyView = null;
        }
    }
}
