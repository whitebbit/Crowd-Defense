using _3._Scripts.Game.Units.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;

namespace _3._Scripts.Game.Units.HitBoxes
{
    public abstract class HitBox: MonoBehaviour, IWeaponVisitor
    {
        [SerializeField] protected Unit unit;
        protected Collider Collider;

        private void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        public abstract void Visit(WeaponConfig config);
    }
}