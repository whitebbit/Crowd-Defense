using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Game.Weapon.Types.MachineGun.FSM
{
    public class MachineGunAttackState : State
    {
        private readonly WeaponConfig _config;
        private float _attackTime;
        private readonly Camera _camera;
        private event Action<int> OnAttack;

        private float Damage => _config.Get<float>("damage") *
                                _config.Improvements.GetDamageImprovement(_config.Get<string>("id"));

        private int BulletsCount => _config.Get<int>("bulletCount") +
                                _config.Improvements.GetAmmoImprovement(_config.Get<string>("id"));
        
        public int CurrentBulletCount { get; private set; }

        public MachineGunAttackState(WeaponConfig config, Action<int> onAttack)
        {
            _camera = Camera.main;
            _config = config;
            CurrentBulletCount = BulletsCount;

            OnAttack += onAttack;
        }

        public override void Update()
        {
            _attackTime -= Time.deltaTime;

            if (!(_attackTime <= 0)) return;

            Shoot();
        }

        public void ResetBulletsCount() => CurrentBulletCount = BulletsCount;

        private void Shoot()
        {
            PerformShot();

            CurrentBulletCount = Mathf.Clamp(CurrentBulletCount - 1, 0, BulletsCount);
            _attackTime = _config.Get<float>("attackTime");
            OnAttack?.Invoke(CurrentBulletCount);
        }

        private void PerformShot()
        {
            for (var i = 0; i < _config.Get<int>("shoutCount"); i++)
            {
                PerformRaycast();
            }
        }

        private void PerformRaycast()
        {
            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            var direction = _config.Get<bool>("useSpread") ? screenCenter + CalculateSpread() : screenCenter;
            var ray = _camera.ScreenPointToRay(direction);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _config.Get<LayerMask>("attackMask")))
            {
                ScanHit(hit);
            }

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }

        private void ScanHit(RaycastHit hit)
        {
            var hitCollider = hit.collider;
            if (hitCollider.TryGetComponent(out IWeaponVisitor visitor))
            {
                Accept(visitor, hit);
            }
        }

        private void Accept(IWeaponVisitor visitor, RaycastHit hit)
        {
            visitor?.Visit(Damage);
        }

        private Vector3 CalculateSpread()
        {
            var spreadFactor = _config.Get<float>("spreadFactor");
            return Random.insideUnitCircle * spreadFactor;
        }
    }
}