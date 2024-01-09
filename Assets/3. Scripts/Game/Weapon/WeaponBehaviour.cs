using System;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Weapon
{
    public abstract class WeaponBehaviour: MonoBehaviour
    {
        [SerializeField] protected WeaponConfig config;
        [SerializeField] protected WeaponObject weaponObject;

        private WeaponFSM _weaponFsm;

        private void Awake()
        {
            _weaponFsm = GetWeaponFSM();
        }

        private void Update()
        {
            _weaponFsm.Update();
        }
        

        protected abstract WeaponFSM GetWeaponFSM();
    }
}