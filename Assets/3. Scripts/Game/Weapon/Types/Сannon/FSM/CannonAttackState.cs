using _3._Scripts.FSM.Base;
using _3._Scripts.FSM.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Сannon.FSM
{
    public class CannonAttackState: State
    {
        private readonly WeaponConfig _config;
        private readonly Missile _cannonball;
        private readonly WeaponObject _weaponObject;
        public int CurrentBulletCount { get; private set; }
        public CannonAttackState(WeaponConfig config, Missile cannonball, WeaponObject weaponObject)
        {
            _config = config;
            _cannonball = cannonball;
            _weaponObject = weaponObject;
            
            CurrentBulletCount = _config.Get<int>("bulletCount");
        }
        
        public override void OnExit()
        {
            Shoot();
        }
        
        public void ResetBulletsCount() => CurrentBulletCount = _config.Get<int>("bulletCount");
        
        private void Shoot()
        {
            PerformShot();
            CurrentBulletCount = Mathf.Clamp(CurrentBulletCount - 1, 0, _config.Get<int>("bulletCount"));
        }

        private void PerformShot()
        {
            var cannonball = Object.Instantiate(_cannonball, _weaponObject.Point.position, _weaponObject.Point.rotation);
            cannonball.Launch(_weaponObject.Point, _config);
        }
    }
}