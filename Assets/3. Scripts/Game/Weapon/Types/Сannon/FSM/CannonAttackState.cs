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
        private readonly Transform _point;
        public int CurrentBulletCount { get; private set; }
        public CannonAttackState(WeaponConfig config, Missile cannonball, Transform point)
        {
            _config = config;
            _cannonball = cannonball;
            _point = point;
            
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
            var cannonball = Object.Instantiate(_cannonball, _point.position, _point.rotation);
            cannonball.Launch(_point, _config);
        }
    }
}