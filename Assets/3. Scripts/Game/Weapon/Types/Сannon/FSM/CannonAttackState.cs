using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.FSM.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _3._Scripts.Game.Weapon.Types.Сannon.FSM
{
    public class CannonAttackState: State
    {
        private readonly WeaponConfig _config;
        private readonly Missile _cannonball;
        private readonly WeaponObject _weaponObject;
        public int CurrentBulletCount { get; private set; }
        private int BulletsCount => _config.Get<int>("bulletCount") +
                                    _config.Improvements.GetAmmoImprovement(_config.Get<string>("id"));
        private event Action<int> OnAttack;

        public CannonAttackState(WeaponConfig config, Missile cannonball, WeaponObject weaponObject,
            Action<int> onAttack)
        {
            _config = config;
            _cannonball = cannonball;
            _weaponObject = weaponObject;
            
            CurrentBulletCount = BulletsCount;

            OnAttack += onAttack;
        }
        
        public override void OnExit()
        {
            Shoot();
        }
        
        public void ResetBulletsCount() => CurrentBulletCount = BulletsCount;
        
        private void Shoot()
        {
            PerformShot();
            CurrentBulletCount = Mathf.Clamp(CurrentBulletCount - 1, 0, BulletsCount);
            OnAttack?.Invoke(CurrentBulletCount);
        }

        private void PerformShot()
        {
            var cannonball = Object.Instantiate(_cannonball, _weaponObject.Point.position, _weaponObject.Point.rotation);
            cannonball.Launch(_weaponObject.Point, _config);
            _weaponObject.SpawnDecals();
        }
    }
}