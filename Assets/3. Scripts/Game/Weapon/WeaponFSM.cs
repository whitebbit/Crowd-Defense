using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;

namespace _3._Scripts.Game.Weapon
{
    public abstract class WeaponFSM: FSMHandler
    {
        protected WeaponConfig Config;
        
        protected WeaponFSM(WeaponConfig config)
        {
            Config = config;
        }

        public virtual void Update()
        {
            StateMachine.Update();
        }

        public virtual void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
    }
}