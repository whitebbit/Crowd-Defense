using System;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Health
{
    public class UnitHealth
    {
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
            if(Health <= 0)
                return;
            
            if(value == 0)
                return;
            
            _currentHealth = Math.Clamp(value, 0, _maxHealth);
            
        }
    }
}