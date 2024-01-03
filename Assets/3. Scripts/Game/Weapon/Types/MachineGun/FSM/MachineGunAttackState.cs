using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.MachineGun.FSM
{
    public class MachineGunAttackState : State
    {
        private readonly WeaponConfig _config;
        private float _attackTime;
        public int CurrentBulletCount { get; private set; }

        public MachineGunAttackState(WeaponConfig config)
        {
            _config = config;
            CurrentBulletCount = _config.GetInteger("bulletCount");
        }
        
        public override void Update()
        {
            _attackTime -= Time.deltaTime;

            if (!(_attackTime <= 0)) return;

            Shoot();
        }

        public void ResetBulletsCount() => CurrentBulletCount = _config.GetInteger("bulletCount");
        
        private void Shoot()
        {
            Debug.Log("Shoot");
            _attackTime = _config.GetFloat("attackTime");
            CurrentBulletCount = Mathf.Clamp(CurrentBulletCount - 1, 0, _config.GetInteger("bulletCount"));
        }
        
        
    }
}