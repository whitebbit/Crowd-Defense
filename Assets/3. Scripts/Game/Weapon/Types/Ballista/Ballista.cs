using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.Ballista.FSM;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista
{
    public class Ballista : WeaponFSM
    {
        public Ballista(WeaponConfig config, WeaponObject weaponObject, Missile arrow) : base(
            config, weaponObject)
        {
            var idle = new BallistaIdleState();
            var attack = new BallistaAttackState(config, arrow, weaponObject, OnAttack);
            var reload = new BallistaReloadState(config, attack.ResetBulletsCount, OnReloadStart);

            AddTransition(idle, new FuncPredicate(() => !Input.GetMouseButton(0) && !reload.Reloading));
            AddTransition(attack,
                new FuncPredicate(() =>
                    Input.GetMouseButton(0) && attack.CurrentBulletCount > 0 && !reload.Reloading));
            AddTransition(reload, new FuncPredicate(() => attack.CurrentBulletCount <= 0 && !reload.Reloading));

            StateMachine.SetState(idle);
        }
    }
}