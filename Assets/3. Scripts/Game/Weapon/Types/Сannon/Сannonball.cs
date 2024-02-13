using System;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Сannon
{
    public class Cannonball : Missile
    {
        [SerializeField] private ParticleSystem explosion;
        private readonly Collider[] _results = new Collider[100];

        public override void Launch(Transform fromPoint, WeaponConfig config)
        {
            Config = config;
            
            var configDir = Config.Get<Vector3>("direction");
            var direction = fromPoint.up * configDir.y + fromPoint.forward * configDir.z +
                            fromPoint.right * configDir.x;
            
            rigidbody.AddForce(direction * Config.Get<float>("cannonForce"), ForceMode.Impulse);
            Destroy(gameObject, 5);
        }

        protected override void OnCollision(Collision other = null)
        {
            Explosion();
        }

        private void Explosion()
        {
            if (Exploded) return;

            var position = transform.position;

            var size = Physics.OverlapSphereNonAlloc(position, Config.Get<float>("explosionRadius"), _results,
                Config.Get<LayerMask>("explosionMask"));

            for (var i = 0; i < size; i++)
            {
                if (_results[i].gameObject.TryGetComponent(out IWeaponVisitor visitor))
                    visitor.Visit(Damage);
            }

            collider.enabled = false;
            Exploded = true;
            AudioManager.Instance.PlayOneShot("boom");
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}