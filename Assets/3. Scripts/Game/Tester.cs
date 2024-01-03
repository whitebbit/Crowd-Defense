using System;
using _3._Scripts.Game.Weapon;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types.MachineGun;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Tester: MonoBehaviour
    {
        [SerializeField] private WeaponConfig config;
        private WeaponFSM _weapon;
        
        private void Awake()
        {
            _weapon = new MachineGun(config);
        }

        private void Update()
        {
            _weapon.Update();
        }

        private void FixedUpdate()
        {
            _weapon.FixedUpdate();
        }
    }
}