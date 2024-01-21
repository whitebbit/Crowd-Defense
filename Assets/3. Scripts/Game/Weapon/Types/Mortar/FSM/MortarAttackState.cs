using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _3._Scripts.Game.Weapon.Types.Mortar.FSM
{
    public class MortarAttackState : State
    {
        private readonly WeaponConfig _config;
        private readonly Missile _explosiveShells;
        private readonly WeaponObject _weaponObject;
        private event Action<int> OnAttack;

        public int CurrentBulletCount { get; private set; }
        private int BulletsCount => _config.Get<int>("bulletCount") +
                                    _config.Improvements.GetAmmoImprovement(_config.Get<string>("id"));
        public MortarAttackState(WeaponConfig config, Missile explosiveShells, WeaponObject weaponObject,
            Action<int> onAttack)
        {
            _config = config;
            _explosiveShells = explosiveShells;
            _weaponObject = weaponObject;

            OnAttack += onAttack;
            
            CurrentBulletCount = BulletsCount;
        }

        public override void OnExit()
        {
            Shoot();
        }

        public void ResetBulletsCount() => CurrentBulletCount =BulletsCount;

        private void Shoot()
        {
            PerformShot();
            CurrentBulletCount = Mathf.Clamp(CurrentBulletCount - 1, 0, BulletsCount);
            OnAttack?.Invoke(CurrentBulletCount);
        }

        private void PerformShot()
        {
            var arrow = Object.Instantiate(_explosiveShells, _weaponObject.Point.position, Quaternion.identity);
            arrow.Launch(_weaponObject.Point, _config);
            _weaponObject.SpawnDecals();
        }
    }
}