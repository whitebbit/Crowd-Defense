using _3._Scripts.FSM.Interfaces;
using _3._Scripts.Game.Weapon.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types
{
    public abstract class Firearms : IWeapon, IReloadable
    {
        protected readonly FirearmsConfig Config;

        protected IPredicate ReloadCondition;
        protected IPredicate AttackCondition;

        private int _currentBulletCount;

        private float _attackTime;

        protected Firearms(FirearmsConfig config)
        {
            Config = config;

            _currentBulletCount = config.BulletCount;
        }

        public void Attack()
        {
            if (!AttackCondition.Evaluate()) return;

            OnAttack();
        }

        public void Update()
        {
            _attackTime = Mathf.Clamp(_attackTime - Time.deltaTime, 0, Config.AttackSpeed);

            OnUpdate();
        }

        public void Reload()
        {
            if (!ReloadCondition.Evaluate()) return;

            OnReload();
        }


        protected void ResetAttackTime() => _attackTime = Config.AttackSpeed;
        protected void ResetBulletCount() => _currentBulletCount = Config.BulletCount;
        protected void ChangeBulletCount(int value)
        {
            _currentBulletCount = Mathf.Clamp(_currentBulletCount + value, 0, Config.BulletCount);
        }

        protected bool CanShoot()
        {
            return _currentBulletCount > 0 && _attackTime <= 0;
        }
        protected bool NoBullets()
        {
            return _currentBulletCount <= 0;
        }
        
        protected abstract void OnAttack();
        protected abstract void OnReload();
        protected virtual void OnUpdate()
        {
        }
    }
}