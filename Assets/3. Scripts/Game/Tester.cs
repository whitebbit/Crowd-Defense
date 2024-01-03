using System;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Weapons;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Tester: MonoBehaviour
    {
        [SerializeField] private FirearmsConfig config;
        private MachineGun _weapon;

        private void Awake()
        {
            _weapon = new MachineGun(config);
        }

        private void Update()
        {
            _weapon.Attack();
            _weapon.Update();
            _weapon.Reload();
        }
    }
}