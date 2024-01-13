using System;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Manager
{
    public static class HealthManager
    {
        public static event Action<int, int> OnChanged;

        public static int HealthCount
        {
            get => YandexGame.savesData.health;
            set
            {
                var count = Mathf.Clamp(value, 0, 100);
                OnChanged?.Invoke(YandexGame.savesData.health, count);
                
                YandexGame.savesData.health = count;
                YandexGame.SaveProgress();
            }
        }
    }
}