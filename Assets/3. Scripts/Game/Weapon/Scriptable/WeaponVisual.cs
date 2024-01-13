using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Weapon Visual", fileName = "WeaponVisual")]
    public class WeaponVisual: ScriptableObject
    {
        [Space] [SerializeField] private Sprite icon;
        [SerializeField] private SerializableDictionary<string, string> titles;
    }
}