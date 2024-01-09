using System;
using System.Collections.Generic;
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
        [SerializeField] private List<WeaponBehaviour> weapons = new();
        private WeaponBehaviour _currentWeapon;
        
    }
}