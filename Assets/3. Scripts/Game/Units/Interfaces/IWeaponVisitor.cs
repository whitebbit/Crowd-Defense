using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Units.Interfaces
{
    public interface IWeaponVisitor
    {
        public void Visit(WeaponConfig config);
    }
}