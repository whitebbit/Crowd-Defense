using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Game.Units.Scriptable;
using _3._Scripts.Game.Weapon.Scriptable;
using net.krej.Singleton;
using UnityEngine;
using YG;

namespace _3._Scripts.Game
{
    public class Configuration: Singleton<Configuration>
    {
        [SerializeField] private List<WeaponConfig> weaponConfigs;
        [Space] [SerializeField] private List<BossConfig> bossConfigs = new();
        
        public List<WeaponConfig> WeaponConfigs => weaponConfigs;
        public BossConfig CurrentBoss => bossConfigs.Find(b => b.ID == GetBossName());
        
        public string GetBossName()
        {
            var bossName = string.IsNullOrEmpty(YandexGame.savesData.currentBossName)
                ? bossConfigs[Random.Range(0, bossConfigs.Count)].ID
                : YandexGame.savesData.currentBossName;
            
            YandexGame.savesData.currentBossName = bossName;
            YandexGame.SaveProgress();
            
            return bossName;
        }

        public void SetNewBoss()
        {
            YandexGame.savesData.currentBossName = GetRandomBossName();
            YandexGame.SaveProgress();
        }
        
        private string GetRandomBossName()
        {
            var list =  bossConfigs.Where(item => item.ID != GetBossName()).ToList();
            var obj = list[Random.Range(0, list.Count)].ID;
            return obj;
        }
    }
}