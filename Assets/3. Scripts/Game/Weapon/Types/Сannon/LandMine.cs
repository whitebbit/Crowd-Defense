using System;
using System.Collections;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Сannon
{
    public class LandMine : Missile
    {
        [SerializeField] private ParticleSystem explosion;
        private readonly Collider[] _results = new Collider[100];
        private readonly Collider[] _scannerResults = new Collider[5];

        private void Update()
        {
            Scanning();
        }

        public override void Launch(Transform fromPoint, WeaponConfig config)
        {
            var direction = fromPoint.up * 0.1f + fromPoint.forward;
            Config = config;

            rigidbody.AddForce(direction * Config.Get<float>("cannonForce"), ForceMode.Impulse);
            //Destroy(gameObject, 5);
        }

        protected override void OnCollision(Collision other = null)
        {
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

        private void Scanning()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, 1.5f,
                _scannerResults,
                Config.Get<LayerMask>("explosionMask"));

            if (size <= 0) return;

            Explosion();
        }
    }
}