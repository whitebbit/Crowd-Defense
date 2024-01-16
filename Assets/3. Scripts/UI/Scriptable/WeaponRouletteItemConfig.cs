using UnityEngine;
using YG;

namespace _3._Scripts.UI.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/UI/Roulette Item Configs/Weapon", fileName = "WeaponRouletteItemConfig")]
    public class WeaponRouletteItemConfig: RouletteItemConfig
    {
        [Space] [SerializeField] private string weaponID;
        public string WeaponID => weaponID;

        public override void GetReward()
        {
            YandexGame.savesData.secondWeapon = weaponID;
        }
    }
}