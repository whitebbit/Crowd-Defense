using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    public abstract class WeaponConfig: ScriptableObject
    {
        [SerializeField] private float attackSpeed;

        public float AttackSpeed => attackSpeed;
    }
}