using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.Mortar.FSM;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Mortar
{
    public class Mortar: WeaponFSM
    {
        public Mortar(WeaponConfig config, WeaponObject weaponObject, Missile explosiveShells) : base(config, weaponObject)
        {
            var idle = new MortarIdleState();
            var attack = new MortarAttackState(config, explosiveShells, weaponObject, OnAttack);
            var reload = new MortarReloadState(config, attack.ResetBulletsCount, OnReloadStart);

            AddTransition(idle, new FuncPredicate(() => !Input.GetMouseButton(0) && !reload.Reloading));
            AddTransition(attack,
                new FuncPredicate(() =>
                    Input.GetMouseButton(0) && attack.CurrentBulletCount > 0 && !reload.Reloading));
            AddTransition(reload, new FuncPredicate(() => attack.CurrentBulletCount <= 0 && !reload.Reloading));

            StateMachine.SetState(idle);
        }
    }
}