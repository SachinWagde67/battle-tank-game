using BattleTank;
using Ground;
using Interfaces;
using TankServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace EnemyServices
{
    public class EnemyView : MonoBehaviour, IDamagable
    {
        [HideInInspector] public float maxX, maxZ, minX, minZ;
        [HideInInspector] public Transform playerTransform;
        [HideInInspector] public bool detectPlayer;
        [HideInInspector] public EnemyStateEnum activeState;
        [HideInInspector] public float timer;
        [HideInInspector] public GameObject dropHealth;
        [SerializeField] private ParticleSystem explosionParticles;
        private Collider ground;
        private ParticleSystem enemyTankExplosion;
        private EnemyFollow followState;
        private EnemyPatrol patrolState;

        public Transform shootPoint;
        public GameObject enemyTurret;
        public EnemyController enemyController;
        public NavMeshAgent agent;
        public float canFire;
        public Slider healthSlider;
        public Image fillImage;
        public MeshRenderer[] enemyChilds;
        public GameObject dropHealthPrefab;

        public EnemyState currentState;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            enemyController.setHealthUI();
            setEnemyTankColor();
            setGround();
            setPlayerTransform();
        }

        private void Update()
        {
            enemyController.Move();
        }

        private void setPlayerTransform()
        {
            playerTransform = TankService.Instance.PlayerPos();
        }

        private void setGround()
        {
            ground = GroundCollider.groundBoxCollider;
            maxX = ground.bounds.max.x;
            maxZ = ground.bounds.max.z;
            minX = ground.bounds.min.x;
            minZ = ground.bounds.min.z;
        }

        public void setEnemyController(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }

        public void InitializeState(EnemyStateEnum initialState)
        {
            switch (initialState)
            {
                case EnemyStateEnum.Follow:
                    currentState = followState;
                    break;

                case EnemyStateEnum.Patrol:
                    currentState = patrolState;
                    break;

                default:
                    currentState = null;
                    break;
            }
            currentState.OnStateEnter();
        }

        public void setEnemyTankColor()
        {
            MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = enemyController.enemyModel.enemyColor;
            }
        }

        public void instantiateTankExplosionParticles()
        {
            enemyTankExplosion = Instantiate(explosionParticles, transform.position, transform.rotation);
            enemyTankExplosion.Play();
        }

        public GameObject instantiateDropHealth()
        {
            Vector3 pos = transform.position;
            dropHealth = Instantiate(dropHealthPrefab, new Vector3(pos.x, pos.y + 1.5f, pos.z), transform.rotation);
            dropHealth.transform.parent = null;
            return dropHealth;
        }

        public void TakeDamage(float damage)
        {
            enemyController.applyDamage(damage);
        }

        public void destroyView()
        {
            for (int i = 0; i < enemyChilds.Length; i++)
            {
                enemyChilds[i] = null;
            }

            shootPoint = null;
            agent = null;
            ground = null;
            dropHealth = null;
            dropHealthPrefab = null;
            playerTransform = null;
         
            if(TankService.Instance.getTankController().tankModel != null)
            {
                Destroy(enemyTankExplosion.gameObject, 0.5f);
            }

            Destroy(this.gameObject);
        }
    }
}
