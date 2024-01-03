using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.MachineGun.FSM
{
    public class MachineGunAttackState : State
    {
        private readonly WeaponConfig _config;
        private float _attackTime;
        private int _currentBulletCount;

        public MachineGunAttackState(WeaponConfig config)
        {
            _config = config;
            _currentBulletCount = _config.GetInteger("bulletCount");
        }
        
        public override void Update()
        {
            _attackTime -= Time.deltaTime;

            if (_currentBulletCount <= 0) return;
            if (!(_attackTime <= 0)) return;

            Debug.Log("Shoot");
            _attackTime = _config.GetFloat("attackTime");
            _currentBulletCount = Mathf.Clamp(_currentBulletCount - 1, 0, _config.GetInteger("bulletCount"));
        }
        
        
    }
}