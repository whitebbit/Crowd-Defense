using _3._Scripts.UI.Manager;
using UnityEngine;

namespace _3._Scripts.UI.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/UI/Roulette Item Configs/Coins", fileName = "CoinsRouletteItemConfig")]
    public class CoinsRouletteItemConfig: RouletteItemConfig
    {
        [Space]
        [SerializeField] private int count;
        
        public override void GetReward()
        {
            MoneyManager.MoneyCount += count;
        }
    }
}