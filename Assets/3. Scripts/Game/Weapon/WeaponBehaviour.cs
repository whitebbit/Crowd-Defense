using System;
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

        private WeaponFSM _weaponFsm;
        private void Awake()
        {
            Config = Configuration.Instance.WeaponConfigs.Find(w => w.Get<string>("id") == id);
            _weaponFsm = GetWeaponFSM();
        }

        private void Update()
        {
            _weaponFsm.Update();
        }

        public void SetState(bool state) => gameObject.SetActive(state);


        protected abstract WeaponFSM GetWeaponFSM();
    }
}