﻿using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.AI.FSM.States
{
    public class BotIdleState : State
    {
        private readonly IAnimator _animator;

        public BotIdleState(IAnimator animator)
        {
            _animator = animator;
        }

        public override void OnEnter()
        {
            _animator.Play("idle");
        }
    }
}