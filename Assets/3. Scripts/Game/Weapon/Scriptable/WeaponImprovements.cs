using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Weapon Improvements", fileName = "WeaponImprovements")]
    public class WeaponImprovements: ScriptableObject
    {
        [SerializeField] private SerializableDictionary<int, float> ammoImprovements;
        [SerializeField] private SerializableDictionary<int, float> damageImprovements;
        [SerializeField] private SerializableDictionary<int, float> reloadImprovements;
    }
}