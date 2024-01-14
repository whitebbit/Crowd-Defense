using UnityEngine;

namespace _3._Scripts.Game.Units.HitBoxes
{
    public class BigUnitHitBox: HitBox
    {
        public override void Visit(float damage)
        {
            unit.Damageable.ApplyDamage(damage);
        }
    }
}