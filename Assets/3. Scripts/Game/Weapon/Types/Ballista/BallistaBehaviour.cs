using UnityEngine;

namespace _3._Scripts.Game.Weapon.Types.Ballista
{
    public class BallistaBehaviour: WeaponBehaviour
    {
        [SerializeField] private Missile arrow;
        protected override WeaponFSM GetWeaponFSM()
        {
            return new Ballista(Config, weaponObject, arrow);
        }
    }
}