using System;
using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using DG.Tweening;
using TMPro;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Manager.Panels
{
    public class PlayPanel : UIPanel
    {
        [Header("Kills Counter")]
        [SerializeField] private TextMeshProUGUI killsCountText;

        [Header("Timer")] [SerializeField] private Image timerImage;
        [SerializeField] private TextMeshProUGUI timerText;

        private Timer _timer;
        private void Start()
        {
            _timer = new Timer(timerImage, timerText, Level.Instance.CompleteLevel);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            UpdateKillsCounter(0);
            Level.Instance.OnKill += UpdateKillsCounter;
            _timer.StartTimer(30);
            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            Level.Instance.OnKill += UpdateKillsCounter;
            _timer.StopTimer();
            base.Close(onComplete, duration);
        }
        
        private void UpdateKillsCounter(int value) => killsCountText.text = $"{value}";

    }
}