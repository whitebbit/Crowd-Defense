using _3._Scripts.FSM.Base;
using _3._Scripts.Game.AI;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Weapon.Scriptable;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.AutoCannon.FSM
{
    public class AutoCannonAttackState : State
    {
        private readonly WeaponConfig _config;
        private readonly WeaponObject _weaponObject;
        private readonly Missile _cannonball;

        private Bot _target;

        public AutoCannonAttackState(WeaponConfig config, WeaponObject weaponObject, Missile cannonball)
        {
            _config = config;
            _weaponObject = weaponObject;
            _cannonball = cannonball;
        }

        public override void Update()
        {
            if (_target == null)
                SetTarget();
        }

        private void SetTarget()
        {
            _target = LevelManager.Instance.CurrentLevel.currentWave.Bots.Find(b => b.Health.Health > 0);

            if (_target == null) return;

            var position = _target.transform.position;
            var targetPos = new Vector3(position.x, position.y, position.z);
            _weaponObject.transform.DOLookAt(targetPos, 1f).OnComplete(Shoot)
                .SetDelay(1);
        }

        private void Shoot()
        {
            var cannonball =
                Object.Instantiate(_cannonball, _weaponObject.Point.position, _weaponObject.Point.rotation);
            cannonball.Launch(_weaponObject.Point, _config);
            _weaponObject.PlayGunshotSound();
            _target = null;
        }
    }
}