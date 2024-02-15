using System;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        public string ID => id;

        [SerializeField] protected string id;
        [SerializeField] protected WeaponObject weaponObject;
        protected WeaponConfig Config;

        public WeaponFSM WeaponFsm { get; private set; }
        private bool _active;

        private void Awake()
        {
            Config = Configuration.Instance.WeaponConfigs.Find(w => w.Get<string>("id") == id);
            if (Config == null)
                Config = Configuration.Instance.AdditionalWeaponConfigs.Find(w => w.Get<string>("id") == id);
            WeaponFsm = GetWeaponFSM();
        }

        private void Update()
        {
            if (!LevelManager.Instance.CurrentLevel.LevelInProgress) return;

            if (!_active) return;

            WeaponFsm.Update();
        }

        public void SetState(bool state)
        {
            _active = state;
            weaponObject.SetState(state);
        }
        public void SetGlobalState(bool state)
        {
            gameObject.SetActive(state);
            SetState(state);
        }

        protected abstract WeaponFSM GetWeaponFSM();
    }
}