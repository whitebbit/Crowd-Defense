using System;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Health
{
    public class UnitHealth
    {
        public event Action<float, float> OnHealthChanged;

        private readonly float _maxHealth;
        private float _currentHealth;

        public float Health
        {
            get => _currentHealth;
            set => SetHealth(value);
        }

        public UnitHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        private void SetHealth(float value)
        {
            if (Health <= 0)
                return;
            
            _currentHealth = Math.Clamp(value, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}