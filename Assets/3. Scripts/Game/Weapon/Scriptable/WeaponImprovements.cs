using UnityEngine;
using YG;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Weapon Improvements", fileName = "WeaponImprovements")]
    public class WeaponImprovements: ScriptableObject
    {
        [SerializeField] private SerializableDictionary<int, int> ammoImprovements;
        [SerializeField] private SerializableDictionary<int, float> damageImprovements;
        [SerializeField] private SerializableDictionary<int, float> reloadImprovements;

        public int GetAmmoImprovement(string id)
        {
            var level = YandexGame.savesData.GetWeaponLevel(id);

            return ammoImprovements[level];
        }
        
        public float GetDamageImprovement(string id)
        {
            var level = YandexGame.savesData.GetWeaponLevel(id);

            return damageImprovements[level];
        }
        
        public float GetReloadImprovement(string id)
        {
            var level = YandexGame.savesData.GetWeaponLevel(id);

            return reloadImprovements[level];
        }
    }
}