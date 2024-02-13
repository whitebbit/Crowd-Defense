using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista
{
    public class Arrow: Missile
    {
        private readonly Collider[] _results = new Collider[100];



        public override void Launch(Transform fromPoint, WeaponConfig config)
        {
            var direction = fromPoint.forward;
            Config = config;

            rigidbody.AddForce(direction * Config.Get<float>("ballistaForce"), ForceMode.Impulse);
        }

        protected override void OnCollision(Collision other = null)
        {
            Explosion();
        }
        
        private void Explosion()
        {
            if (Exploded) return;

            var position = transform.position;
            var size = Physics.OverlapSphereNonAlloc(position, Config.Get<float>("explosionRadius"), _results, Config.Get<LayerMask>("explosionMask"));

            for (var i = 0; i < size; i++)
            {
                if (_results[i].gameObject.TryGetComponent(out IWeaponVisitor visitor))
                    visitor.Visit(Damage);
            }

            collider.enabled = false;
            Exploded = true;

            Destroy(gameObject);
        }
    }
}