using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Mortar
{
    public class MortarBehaviour: WeaponBehaviour
    {
        [SerializeField] private Missile explosiveShells;

        protected override WeaponFSM GetWeaponFSM()
        {
            return new Mortar(config, weaponObject, explosiveShells);
        }
    }
}