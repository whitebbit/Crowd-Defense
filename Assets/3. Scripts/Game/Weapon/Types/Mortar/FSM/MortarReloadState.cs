using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Mortar.FSM
{
    public class MortarReloadState: State
    {
        private readonly WeaponConfig _config;
        private float _reloadTime;
        private event Action OnReload;
        
        public bool Reloading { get; private set; }
        public MortarReloadState(WeaponConfig config, Action onReload)
        {
            _config = config;
            OnReload += onReload;
        }

        public override void OnEnter()
        {
            Reloading = true;
            _reloadTime = _config.Get<float>("reloadTime");
        }

        public override void Update()
        {
            _reloadTime -= Time.deltaTime;

            if(_reloadTime > 0) return;
            
            OnReload?.Invoke();
            Reloading = false;
        }
    }
}