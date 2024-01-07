using _3._Scripts.Game.Units.Animations;
using _3._Scripts.Game.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Game.Units.Dying
{
    public class BotSimpleDying : IDying
    {
        private readonly Rigidbody _rigidbody;
        private readonly IAnimator _animations;

        public BotSimpleDying(Rigidbody rigidbody, IAnimator animations)
        {
            _rigidbody = rigidbody;
            _animations = animations;
        }

        public void Dead()
        {
            _animations.PlayRandom("death");
            Punch();
        }

        private void Punch()
        {
            var randVector = new Vector3(Random.Range(-0.1f, 0.1f), 1, 0);
            var randTorqueVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            var randForce = Random.Range(10, 25);
            var randTorqueForce = Random.Range(100, 200);

            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(randVector * randForce, ForceMode.Impulse);
            _rigidbody.AddTorque(randTorqueVector * randTorqueForce, ForceMode.Impulse);
        }
    }
}