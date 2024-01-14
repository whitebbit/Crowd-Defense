using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.MachineGun.FSM
{
    public class MachineGunReloadState : State
    {
        private readonly WeaponConfig _config;
        private float _reloadTime;
        private event Action OnReload;
        private event Action<float> OnReloadStart;

        private float ReloadTime => _config.Get<float>("reloadTime") *
                                    _config.Improvements.GetReloadImprovement(_config.Get<string>("id"));

        public bool Reloading { get; private set; }

        public MachineGunReloadState(WeaponConfig config, Action onReload, Action<float> onReloadStart = null)
        {
            _config = config;
            OnReload += onReload;
            OnReloadStart += onReloadStart;
        }

        public override void OnEnter()
        {
            OnReloadStart?.Invoke(ReloadTime);
            Reloading = true;
            _reloadTime = ReloadTime;
        }

        public override void Update()
        {
            _reloadTime -= Time.deltaTime;

            if (_reloadTime > 0) return;

            OnReload?.Invoke();
            Reloading = false;
        }
    }
}