using _3._Scripts.Game.Units;
using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Damageable;
using _3._Scripts.Game.Units.Dying;
using _3._Scripts.Game.Units.Health;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Units.Scriptable.Animations;
using FSG.MeshAnimator;
using UnityEngine;


namespace _3._Scripts.Game.AI
{
    public class Bot : Unit
    {
        [Header("Parameters")] [SerializeField]
        private int health = 100;

        [SerializeField] private float speed = 3;
        [SerializeField] private bool defaultBot = true;
        [Header("Settings")] [SerializeField] private MeshAnimatorBase animator;
        [SerializeField] private MeshAnimationsHolder animations;

        private BotFSM _botFsm;
        private IAnimator _animator;

        protected override void OnAwake()
        {
            _animator = new BotAnimations(animator, animations);

            var ragdoll = new Ragdoll(transform);
            var dying = new BotRagdollDying(ragdoll, _animator, defaultBot, () => Destroy(gameObject, 3));

            Health = new UnitHealth(health);
            Damageable = new UnitDamageable(Health);

            _botFsm = new BotFSM(transform, speed, _animator, Health, dying, () => Destroy(gameObject, 3));
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