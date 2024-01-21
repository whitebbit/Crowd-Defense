using System;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Manager
{
    public static class KeysManager
    {
        public static event Action<int> OnChanged;

        public static int KeysCount
        {
            get => YandexGame.savesData.keys;
            set
            {
                var count = Mathf.Clamp(value, 0, 3);
                OnChanged?.Invoke(count);
                
                YandexGame.savesData.keys = count;
                YandexGame.SaveProgress();
            }
        }
    }
}