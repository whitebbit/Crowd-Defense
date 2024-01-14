using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;

namespace _3._Scripts.Game.Weapon
{
    public abstract class WeaponFSM: FSMHandler
    {
        public event Action<int> onAttack;
        public event Action<float> onReloadStart;
        
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

        protected virtual void OnReloadStart(float time)
        {
            onReloadStart?.Invoke(time);
        }

        protected virtual void OnAttack(int value)
        {
            onAttack?.Invoke(value);
        }
    }
}