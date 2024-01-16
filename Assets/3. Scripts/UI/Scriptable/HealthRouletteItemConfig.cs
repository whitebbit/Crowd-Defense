using _3._Scripts.UI.Manager;
using UnityEngine;

namespace _3._Scripts.UI.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/UI/Roulette Item Configs/Health", fileName = "HealthRouletteItemConfig")]
    public class HealthRouletteItemConfig: RouletteItemConfig
    {
        [Space]
        [SerializeField] private int count;
        public override void GetReward()
        {
            HealthManager.HealthCount += count;
        }
    }
}