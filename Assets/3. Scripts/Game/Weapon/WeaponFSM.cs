using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;

namespace _3._Scripts.Game.Weapon
{
    public abstract class WeaponFSM: FSMHandler
    {
        protected WeaponConfig Config;
        private readonly WeaponObject _weaponObject;
        protected WeaponFSM(WeaponConfig config, WeaponObject weaponObject)
        {
            Config = config;
            _weaponObject = weaponObject;
        }
        
        public void Update()
        {
            StateMachine.Update();
        }

        public  void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
    }
}