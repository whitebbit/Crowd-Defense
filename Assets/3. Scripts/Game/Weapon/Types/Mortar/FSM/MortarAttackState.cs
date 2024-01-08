using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Mortar.FSM
{
    public class MortarAttackState: State
    {
        private readonly WeaponConfig _config;
        private readonly Missile _explosiveShells;
        private readonly Transform _point;
        public int CurrentBulletCount { get; private set; }
        public MortarAttackState(WeaponConfig config, Missile explosiveShells, Transform point)
        {
            _config = config;
            _explosiveShells = explosiveShells;
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
            var arrow = Object.Instantiate(_explosiveShells, _point.position, Quaternion.identity);
            arrow.Launch(_point, _config);
        }
    }
}