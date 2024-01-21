using UnityEngine;
using YG;

namespace _3._Scripts.UI.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/UI/Roulette Item Configs/Weapon Unlocker", fileName = "WeaponUnlockerItemConfig")]
    public class WeaponUnlockerItemConfig: WeaponRouletteItemConfig
    {
        public override void GetReward()
        {
            YandexGame.savesData.unlockedWeapons.Add(weaponID);
            YandexGame.SaveProgress();
        }
    }
}