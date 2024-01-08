using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista.FSM
{
    public class BallistaAttackState: State
    {
        private readonly WeaponConfig _config;
        private readonly Missile _arrow;
        private readonly Transform _point;
        public int CurrentBulletCount { get; private set; }
        public BallistaAttackState(WeaponConfig config, Missile arrow, Transform point)
        {
            _config = config;
            _arrow = arrow;
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
            var arrow = Object.Instantiate(_arrow, _point.position, _point.rotation);
            arrow.Launch(_point, _config);
        }
    }
}