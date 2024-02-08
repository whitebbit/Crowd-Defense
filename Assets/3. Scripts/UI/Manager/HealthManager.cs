using System;
using Unity.Mathematics;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Manager
{
    public static class HealthManager
    {
        public static event Action<float, float> OnChanged;

        public static float HealthCount
        {
            get => YandexGame.savesData.health;
            set
            {
                var count = Mathf.Clamp(value, 0f, 150f);
                OnChanged?.Invoke(YandexGame.savesData.health, count);
                
                YandexGame.savesData.health = count;
                YandexGame.SaveProgress();
            }
        }
    }
}