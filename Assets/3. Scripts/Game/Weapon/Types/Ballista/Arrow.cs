using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista
{
    public class Arrow: Missile
    {
        public override void Launch(Transform fromPoint, WeaponConfig config)
        {
            var direction = fromPoint.forward;
            Config = config;

            rigidbody.AddForce(direction * Config.Get<float>("ballistaForce"), ForceMode.Impulse);
        }

        protected override void OnCollision()
        {
            Explosion();
        }
        
        private void Explosion()
        {
            if (Exploded) return;

            var position = transform.position;
            var colliders = Physics.OverlapSphere(position, Config.Get<float>("explosionRadius"),
                Config.Get<LayerMask>("explosionMask"));

            foreach (var c in colliders)
            {
                if (c.gameObject.TryGetComponent(out IWeaponVisitor visitor))
                    visitor.Visit(Config);
            }

            collider.enabled = false;
            Exploded = true;

            Destroy(gameObject);
        }
    }
}