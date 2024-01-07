using System;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Damageable
{
    public class UnitDamageable: IDamageable
    {
        private readonly UnitHealth _health;
        public event Action<float> OnDamageApplied; 
        
        public UnitDamageable(UnitHealth health)
        {
            _health = health;
        }

        public void ApplyDamage(float damage)
        {
            if(!DamageNotNull(damage))
                return;
            
            
            _health.Health -= damage;
            OnDamageAppliedEvent(damage);
        }

        private bool DamageNotNull(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            return true;
        }

        private void OnDamageAppliedEvent(float damage)
        {
            OnDamageApplied?.Invoke(damage);
        }
    }
}