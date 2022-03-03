using UnityEngine;
using TankSO;

namespace TankServices
{
    public class TankService : SingletonGeneric<TankService>
    {
        private TankController tankController;
       
        public ScriptableObjectList tankList;
        public Camera cam;
        public Joystick tankMovementJoystick;
        public Joystick turretRotateJoystick;
        public bool playOnPC;
        public TankScriptableObjects tankScriptableObjects { get; set; }
        public TankView tankView { get; set; }
        
        [HideInInspector] public Transform playerPos;
        
        private void Start()
        {
            CreateNewTank();
            tankController.setCameraReference(cam);
            setTankJoystick();
        }

        private TankController CreateNewTank()
        {
            int random = Random.Range(0, tankList.tank.Length);
            tankScriptableObjects = tankList.tank[random];
            tankView = tankScriptableObjects.tankView;
            TankModel tankModel = new TankModel(tankScriptableObjects);
            tankController = new TankController(tankModel, tankView);
            return tankController;
        }

        public void setTankJoystick()
        {
            if (tankController != null)
            {
                tankController.setJoysticks(tankMovementJoystick, turretRotateJoystick);
            }
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