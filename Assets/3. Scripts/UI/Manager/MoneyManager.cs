using System;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Manager
{
    public static class MoneyManager
    {
        public static event Action<float, float> OnChanged;

        public static int MoneyCount
        {
            get => YandexGame.savesData.money;
            set
            {
                var count = Mathf.Clamp(value, 0, int.MaxValue);
                OnChanged?.Invoke(YandexGame.savesData.money, count);
                
                YandexGame.savesData.money = count;
                YandexGame.SaveProgress();
            }
        }
    }
}