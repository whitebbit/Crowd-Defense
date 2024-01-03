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
            var reload = new MachineGunReloadState(config, attack.ResetBulletsCount);

            AddTransition(idle, new FuncPredicate(() => !Input.GetMouseButton(0) && !reload.Reloading));
            AddTransition(attack,
                new FuncPredicate(() =>
                    Input.GetMouseButton(0) && attack.CurrentBulletCount > 0 && !reload.Reloading));
            AddTransition(reload, new FuncPredicate(() => attack.CurrentBulletCount <= 0 && !reload.Reloading));

            StateMachine.SetState(idle);
        }
    }
}