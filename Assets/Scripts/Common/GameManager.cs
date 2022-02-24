using System.Collections.Generic;
using UnityEngine;
using TankServices;
using EnemyServices;

namespace GamePlayServices
{
    public class GameManager : SingletonGeneric<GameManager>
    {

        public void DestroyAllObjects()
        {
            destroyAllEnemies();
            destroyObjects();
            destroyGround();
        }

        private async void destroyAllEnemies()
        {
            await new WaitForSeconds(1f);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyTank");
            for (int i = 0; i < enemies.Length; i++)
            {
                if (EnemyService.Instance.enemies[i].enemyView != null)
                {
                    enemies[i].GetComponent<EnemyView>().enemyController.enemyDead();
                    await new WaitForSeconds(0.2f);
                }
            }
        }

        private async void destroyObjects()
        {
            await new WaitForSeconds(3f);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("object");

            for(int i = objects.Length - 1; i >= 0; i--)
            {
                Destroy(objects[i]);
                await new WaitForSeconds(0.2f);
            }
        }

        private async void destroyGround()
        {
            await new WaitForSeconds(5f);

            GameObject[] ground = GameObject.FindGameObjectsWithTag("ground");

            for (int i = ground.Length - 1; i >= 0; i--)
            {
                Destroy(ground[i]);
                await new WaitForSeconds(0.2f);
            }
        }
    }
}
