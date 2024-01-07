using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Animations
{
    public class UnitAnimations : IAnimator
    {
        private readonly Animator _animator;

        public UnitAnimations(Animator animator)
        {
            _animator = animator;
        }
        
        public void State(bool state)
        {
            _animator.enabled = state;
        }

        public void Play(string key)
        {
            throw new System.NotImplementedException();
        }

        public void PlayRandom(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}