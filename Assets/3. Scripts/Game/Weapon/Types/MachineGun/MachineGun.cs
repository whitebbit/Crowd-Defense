using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.MachineGun.FSM;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.MachineGun
{
    public class MachineGun : WeaponFSM
    {
        public MachineGun(WeaponConfig config) : base(config)
        {
            var idle = new MachineGunIdleState();
            var attack = new MachineGunAttackState(config);

            AddTransition(idle, new FuncPredicate(() => !Input.GetMouseButton(0)));
            AddTransition(attack, new FuncPredicate(() => Input.GetMouseButton(0)));

            StateMachine.SetState(idle);
        }
    }
}