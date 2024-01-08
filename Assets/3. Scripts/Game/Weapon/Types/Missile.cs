using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types
{
    public abstract class Missile: MonoBehaviour
    {
        [SerializeField] protected Rigidbody rigidbody;
        [SerializeField] protected Collider collider;
        protected WeaponConfig Config;
        protected bool Exploded;

        public abstract void Launch(Transform fromPoint, WeaponConfig config);
        
        private void OnCollisionEnter(Collision other)
        {
            OnCollision();
        }

        protected abstract void OnCollision();
    }
}