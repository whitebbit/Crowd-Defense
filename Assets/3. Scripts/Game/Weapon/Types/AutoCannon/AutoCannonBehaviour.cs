using System;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.AutoCannon
{
    public class AutoCannonBehaviour: WeaponBehaviour
    {
        [SerializeField] private Missile cannonball;
        
        protected override WeaponFSM GetWeaponFSM()
        {
            return new AutoCannon(Config, weaponObject, cannonball);
        }
    }
}