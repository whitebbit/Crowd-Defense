using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Weapon Visual", fileName = "WeaponVisual")]
    public class WeaponVisual: ScriptableObject
    {
        [Space] [SerializeField] private Sprite icon;
        [SerializeField] private SerializableDictionary<string, string> titles;

        public Sprite Icon => icon;

        public string GetTitle(string lang)
        {
            return titles.TryGetValue(lang, out var title) ? title : titles["en"];
        }
    }
}