using System;
using _3._Scripts.Architecture.Helpers;
using _3._Scripts.Architecture.Scriptable.Animations;
using _3._Scripts.TweenAnimations;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Game.AI
{
    public class Bot: MonoBehaviour
    {
        [SerializeField] private AnimationObject fearAnimation;
        [SerializeField] private OverlapFounder overlapFounder;
        private BotFSM _botFsm;
        private void Awake()
        {
            var navMesh = GetComponent<NavMeshAgent>();
            
            _botFsm = new BotFSM(fearAnimation, overlapFounder, navMesh);
        }

        private void Update()
        {
            _botFsm.Update();
        }

        private void FixedUpdate()
        {
            _botFsm.FixedUpdate();
        }
    }
}