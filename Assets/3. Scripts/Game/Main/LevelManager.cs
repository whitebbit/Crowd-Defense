﻿using System.Collections.Generic;
using _3._Scripts.Architecture;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Main
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Defaults")] [SerializeField] private List<Level> defaultLevels = new();
        [Header("Boss")] [SerializeField] private List<Level> bossLevels;

        public Level CurrentLevel { get; private set; }

        public Level CreateLevel(int number)
        {
            if (number > 8)
            {
                number = 1;
                YandexGame.savesData.currentLevel = number;
                YandexGame.SaveProgress();
            }

            var list = number == 8 ? bossLevels : defaultLevels;
            var level = number switch
            {
                8 => Random.Range(0, bossLevels.Count),
                not 8 => YandexGame.savesData.completedLevelsCount >= defaultLevels.Count
                    ? Random.Range(0, defaultLevels.Count)
                    : number - 1
            };
            
            CurrentLevel = Instantiate(list[level], transform);

            return CurrentLevel;
        }

        public void DeleteLevel()
        {
            Destroy(CurrentLevel!.gameObject);
        }
    }
}