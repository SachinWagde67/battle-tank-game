using System.Collections.Generic;
using UnityEngine;
using TankSO;
using EnemyServices;
using GamePlayServices;

namespace TankServices
{
    public class TankService : SingletonGeneric<TankService>
    {
        private TankController tankController;
        
        public ScriptableObjectList tankList;
        public Camera cam;
        public TankScriptableObjects tankScriptableObjects { get; set; }
        public TankView tankView { get; set; }
        //private List<TankController> tanks = new List<TankController>();
        
        [HideInInspector] public Transform playerPos;
        
        private void Start()
        {
            CreateNewTank();
            tankController.setCameraReference(cam);
        }

        private TankController CreateNewTank()
        {
            int random = Random.Range(0, tankList.tank.Length);
            tankScriptableObjects = tankList.tank[random];
            tankView = tankScriptableObjects.tankView;
            TankModel tankModel = new TankModel(tankScriptableObjects);
            tankController = new TankController(tankModel, tankView);
            //tanks.Add(tankController);
            return tankController;
        }

        public TankController getTankController()
        {
            return tankController;
        }

        public void getPlayerPos(Transform _playerPos)
        {
            playerPos = _playerPos;
        }

        public Transform PlayerPos()
        {
            return playerPos;
        }
    }
}