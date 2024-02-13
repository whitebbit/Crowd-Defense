using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Weapon.Types
{
    public abstract class Missile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rigidbody;
        [SerializeField] protected Collider collider;
        protected WeaponConfig Config;
        protected bool Exploded;
        protected float Damage => Config.Get<float>("damage") * Config.Improvements.GetDamageImprovement(Config.Get<string>("id"));


        public abstract void Launch(Transform fromPoint, WeaponConfig config);

        private void OnCollisionEnter(Collision other)
        {
            OnCollision(other);
        }

        protected abstract void OnCollision(Collision other = null);
    }
}