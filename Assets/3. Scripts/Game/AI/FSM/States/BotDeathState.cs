using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotDeathState: State
    {
        private readonly IDying _dying;

        public bool IsDead { get; private set; }

        public BotDeathState(IDying dying)
        {
            _dying = dying;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            IsDead = true;
            _dying.Dead();
            LevelManager.Instance.CurrentLevel.KillBot();
        }
        
    }
}