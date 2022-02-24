using EnemyServices;
using System;
using System.Collections;
using System.Collections.Generic;
using TankServices;
using UnityEngine;
using AudioServices;

namespace BulletServices
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem shellExplosionParticles;
        
        public BulletController bulletController { get; private set; }

        public void SetBulletController(BulletController _bulletController)
        {
            bulletController = _bulletController;
        }

        private void FixedUpdate()
        {
            bulletController.BulletMove();
            Destroy(gameObject,2f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(bulletController.bulletModel.bulletType == BulletType.Enemy && other.gameObject.GetComponent<TankView>() != null)
            { 
                other.gameObject.GetComponent<TankView>().TakeDamage(bulletController.bulletModel.Damage);
                DestroyBullet();
            }
            else if(bulletController.bulletModel.bulletType != BulletType.Enemy && other.gameObject.GetComponent<EnemyView>() != null)
            {
                other.gameObject.GetComponent<EnemyView>().TakeDamage(bulletController.bulletModel.Damage);
                DestroyBullet();
            }
            else if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("object"))
            {
                DestroyBullet();
            }
        }


        public void DestroyBullet()
        {
            ParticleSystem shellExplosion = Instantiate(shellExplosionParticles, transform.position, transform.rotation);
            shellExplosion.Play();
            AudioManager.Instance.shellExplosionAudio.GetComponent<AudioSource>().Play();
            Destroy(shellExplosion.gameObject, 1f);
            Destroy(gameObject);
        }
    }
}
