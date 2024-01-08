﻿using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.Сannon.FSM;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Сannon
{
    public class Cannon : WeaponFSM
    {
        public Cannon(WeaponConfig config, Missile cannonball, Transform point) : base(config)
        {
            var idle = new CannonIdleState();
            var attack = new CannonAttackState(config, cannonball, point);
            var reload = new CannonReloadState(config, attack.ResetBulletsCount);

            AddTransition(idle, new FuncPredicate(() => !Input.GetMouseButton(0) && !reload.Reloading));
            AddTransition(attack,
                new FuncPredicate(() =>
                    Input.GetMouseButtonDown(0) && attack.CurrentBulletCount > 0 && !reload.Reloading));
            AddTransition(reload, new FuncPredicate(() => attack.CurrentBulletCount <= 0 && !reload.Reloading));

            StateMachine.SetState(idle);
        }
    }
}