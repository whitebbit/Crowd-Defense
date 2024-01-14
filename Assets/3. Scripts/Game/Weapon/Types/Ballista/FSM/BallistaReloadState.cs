using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista.FSM
{
    public class BallistaReloadState : State
    {
        private readonly WeaponConfig _config;
        private float _reloadTime;
        private event Action OnReload;
        private event Action<float> OnReloadStart;

        private float ReloadTime => _config.Get<float>("reloadTime") *
                                    _config.Improvements.GetReloadImprovement(_config.Get<string>("id"));

        public bool Reloading { get; private set; }

        public BallistaReloadState(WeaponConfig config, Action onReload, Action<float> onReloadStart)
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