using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Units.HitBoxes
{
    public class SmallUnitHitBox: HitBox
    {
        public override void Visit(float damage)
        {
            unit.Damageable.ApplyDamage(10000000);
        }
    }
}