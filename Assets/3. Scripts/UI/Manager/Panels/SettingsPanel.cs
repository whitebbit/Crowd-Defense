using System;
using _3._Scripts.Game;
using _3._Scripts.UI.Enums;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Manager.Panels
{
    public class SettingsPanel : UIPanel
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Button close;

        private void Start()
        {
            slider.value = YandexGame.savesData.volume;
            slider.onValueChanged.AddListener(ChangeVolume);
            close.onClick.AddListener(()=> UIManager.Instance.CurrentState = UIState.Main);
        }
        
        private void ChangeVolume(float value)
        {
            AudioManager.Instance.Volume = value;
        }
    }
}