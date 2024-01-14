using System;
using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using DG.Tweening;
using TMPro;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Manager.Panels
{
    public class PlayPanel : UIPanel
    {
        [Header("Kills Counter")] [SerializeField]
        private TextMeshProUGUI killsCountText;

        [Header("Timer")] [SerializeField] private Image timerImage;
        [SerializeField] private TextMeshProUGUI timerText;

        [Header("Weapons")] [SerializeField] private WeaponSelector mainWeapon;
        [SerializeField] private WeaponSelector secondWeapon;


        private WeaponSelector _currentWeaponSelector;
        private Timer _timer;

        protected override void Awake()
        {
            base.Awake();
            _timer = new Timer(timerImage, timerText, Level.Instance.CompleteLevel);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            SetWeapons();
            _timer.StartTimer(10);

            Level.Instance.OnKill += UpdateKillsCounter;
            UpdateKillsCounter(0);
            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            Level.Instance.OnKill += UpdateKillsCounter;
            _timer.StopTimer();
            base.Close(onComplete, duration);
        }

        private void UpdateKillsCounter(int value) => killsCountText.text = $"{value}";

        private void SetWeapons()
        {
            var current = YandexGame.savesData.currentWeapon;
            var second = YandexGame.savesData.secondWeapon;

            mainWeapon.OnSelect += ChangeWeapon;
            mainWeapon.Initialize(current);
            mainWeapon.Select();

            secondWeapon.OnSelect += ChangeWeapon;
            secondWeapon.Initialize(second);
            secondWeapon.Unselect();
        }

        private void ChangeWeapon(WeaponSelector weaponSelector)
        {
            if (_currentWeaponSelector != null)
                _currentWeaponSelector.Unselect();

            _currentWeaponSelector = weaponSelector;
        }
    }
}