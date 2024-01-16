using System;
using System.Collections.Generic;
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

        [Header("Timer")] [SerializeField] private TextMeshProUGUI timerText;
        [Header("Goal")] [SerializeField] private TextMeshProUGUI goalText;
        [Header("Weapons")] [SerializeField] private WeaponSelector mainWeapon;
        [SerializeField] private WeaponSelector secondWeapon;
        [Header("Other")] [SerializeField] private List<CanvasGroup> afterGoalObjects = new();

        private WeaponSelector _currentWeaponSelector;
        private Timer _timer;

        protected override void Awake()
        {
            base.Awake();
            _timer = new Timer(text: timerText);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            GameObjectsState(false);
            UpdateKillsCounter(0);
            SetWeapons();

            LevelManager.Instance.CurrentLevel.OnKill += UpdateKillsCounter;
            _timer.OnTime += LevelManager.Instance.CurrentLevel.CompleteLevel;

            base.Open(() =>
            {
                OnLevelStart();
                onComplete?.Invoke();
            }, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            LevelManager.Instance.CurrentLevel.OnKill -= UpdateKillsCounter;
            _timer.OnTime -= LevelManager.Instance.CurrentLevel.CompleteLevel;

            _timer.StopTimer();

            mainWeapon.Unselect();
            mainWeapon.ResetSelector();
            secondWeapon.Unselect();
            secondWeapon.ResetSelector();

            base.Close(onComplete, duration);
        }

        private void OnLevelStart()
        {
            goalText.DOFade(0, 0);
            Transition.Instance.Open(0.3f).OnComplete(() =>
            {
                goalText.DOFade(1, 0.25f).OnComplete(() =>
                {
                    goalText.DOFade(0, 0.25f).SetDelay(1f).OnComplete(() =>
                    {
                        _timer.StartTimer(30);
                        GameObjectsState(true);
                        LevelManager.Instance.CurrentLevel.StartLevel();
                    });
                });
            });
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

        private void GameObjectsState(bool state)
        {
            if (state)
            {
                foreach (var obj in afterGoalObjects)
                {
                    obj.DOFade(1, .25f);
                }
            }
            else
            {
                foreach (var obj in afterGoalObjects)
                {
                    obj.alpha = 0;
                }
            }
        }
    }
}