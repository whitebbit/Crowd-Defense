using _3._Scripts.Extensions;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Manager;
using UnityEngine;

namespace _3._Scripts.Game.AI
{
    public class BigBot : Bot
    {
        [Header("Big bot Settings")] [SerializeField]
        private HealthBar healthBar;
        [Header("Key setting")]
        [SerializeField, Range(0, 100)] private float chance;
        [SerializeField] private Transform key;

        protected override void OnStart()
        {
            AddKey();
            Health.OnHealthChanged += healthBar.Change;
            Health.OnHealthChanged += GiveKey;
        }

        private void GiveKey(float current, float max)
        {
            if (current > 0) return;
            if (!key.gameObject.activeSelf) return;
            
            key.gameObject.SetActive(false);
            KeysManager.KeysCount++;
        }

        private void AddKey()
        {
            key.gameObject.SetActive(false);
            
            if (chance.DropChance())
                key.gameObject.SetActive(true);
        }
        
    }
}