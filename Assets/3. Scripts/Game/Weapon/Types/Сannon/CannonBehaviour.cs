using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Сannon
{
    public class CannonBehaviour: WeaponBehaviour
    {
        [SerializeField] private Missile cannonball;
        protected override WeaponFSM GetWeaponFSM()
        {
            return new Cannon(config, weaponObject, cannonball);
        }
    }
}