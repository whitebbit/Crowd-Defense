using _3._Scripts.UI.Components;
using UnityEngine;

namespace _3._Scripts.Game.AI
{
    public class BigBot: Bot
    {
        [Header("Big bot Settings")] [SerializeField]
        private HealthBar healthBar;
        protected override void OnStart()
        {
            Health.OnHealthChanged += healthBar.Change;
        }
    }
}