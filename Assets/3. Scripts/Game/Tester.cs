using System;
using _3._Scripts.Game.Weapon;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types;
using _3._Scripts.Game.Weapon.Types.Ballista;
using _3._Scripts.Game.Weapon.Types.MachineGun;
using _3._Scripts.Game.Weapon.Types.Mortar;
using _3._Scripts.Game.Weapon.Types.Сannon;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Tester: MonoBehaviour
    {
        [SerializeField] private WeaponConfig config;
        [SerializeField] private Missile missile;
        [SerializeField] private Transform point;
        private WeaponFSM _weapon;
        
        private void Awake()
        {
            _weapon = new Mortar(config, missile, point);
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