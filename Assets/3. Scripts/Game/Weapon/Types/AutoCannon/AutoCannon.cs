using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.AutoCannon.FSM;
using _3._Scripts.Game.Weapon.Types.Ballista.FSM;

namespace _3._Scripts.Game.Weapon.Types.AutoCannon
{
    public class AutoCannon : WeaponFSM
    {
        public AutoCannon(WeaponConfig config, WeaponObject weaponObject, Missile cannonball) : base(config,
            weaponObject)
        {
            var attack = new AutoCannonAttackState(config, weaponObject, cannonball);

            AddTransition(attack, new FuncPredicate(() => true));
            StateMachine.SetState(attack);
        }
    }
}