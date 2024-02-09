using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Mortar
{
    public class ExplosiveShells : Missile
    {        
        [SerializeField] private ParticleSystem explosion;
        public override void Launch(Transform fromPoint, WeaponConfig config)
        {
            var forward = fromPoint.forward;
            var direction = forward * .75f + transform.up;
            Config = config;

            rigidbody.AddForce(direction * Config.Get<float>("mortarForce"), ForceMode.Impulse);
            rigidbody.AddTorque(transform.right * Config.Get<float>("mortarTorque"), ForceMode.Acceleration);
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