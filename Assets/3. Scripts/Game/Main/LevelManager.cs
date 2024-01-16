using System.Collections.Generic;
using _3._Scripts.Architecture;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Main
{
    public class LevelManager: Singleton<LevelManager>
    {
        [Header("Defaults")]
        [SerializeField] private List<Level> defaultLevels = new();
        [Header("Boss")]
        [SerializeField] private SerializableDictionary<string, Level> bossLevels;
        
        public Level CurrentLevel { get; private set; }
        public Level CreateLevel(int number)
        {
            if (number > 8)
            {
                number = 1;
                YandexGame.savesData.currentLevel = number;
                YandexGame.SaveProgress();
            }
            
            if (number == 8)
                CurrentLevel = Instantiate(bossLevels[Configuration.Instance.GetBossName()], transform);
            else
            {
                var level = Random.Range(0, defaultLevels.Count);
                CurrentLevel = Instantiate(defaultLevels[level], transform);
            }

            return CurrentLevel;
        }

        public void DeleteLevel()
        {
            Destroy(CurrentLevel!.gameObject);
        }
    }
}