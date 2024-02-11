﻿using System;
using System.Collections.Generic;
using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Manager.Popups;
using DG.Tweening;
using TMPro;
using UI.Panels;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private BossPopup bossPopup;
        [SerializeField] private LosePopup losePopup;
        [SerializeField] private Tutorial tutorial;
        [SerializeField] private Image crosshair;

        private WeaponSelector _currentWeaponSelector;

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            GameObjectsState(false);
            UpdateKillsCounter(0);
            SetWeapons();

            crosshair.DOFade(0, 0);
            tutorial.StartTutorial();

            tutorial.OnStop += ShowCrosshair;
            HealthManager.OnChanged += TryLoseLevel;
            LevelManager.Instance.CurrentLevel.OnKill += UpdateKillsCounter;

            base.Open(() =>
            {
                OnLevelStart();
                onComplete?.Invoke();
            }, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            LevelManager.Instance.CurrentLevel.OnKill -= UpdateKillsCounter;
            HealthManager.OnChanged -= TryLoseLevel;
            tutorial.OnStop -= ShowCrosshair;
            
            mainWeapon.Unselect();
            mainWeapon.ResetSelector();
            secondWeapon.Unselect();
            secondWeapon.ResetSelector();
            losePopup.Close();

            base.Close(onComplete, duration);
        }

        private void ShowCrosshair() => crosshair.DOFade(1, 0.25f);

        private void OnLevelStart()
        {
            goalText.DOFade(0, 0);
            Transition.Instance.Open(0.2f);
            ViewLevelGoal();
        }

        private void TryLoseLevel(float _, float newValue)
        {
            if (newValue > 0) return;

            LevelManager.Instance.CurrentLevel.LoseLevel();
            
            losePopup.Open();
        }

        private void ViewLevelGoal()
        {
            if (YandexGame.savesData.currentLevel == 8)
                ViewBossPopup();
            else
                ViewClassicPopup();
        }

        private void ViewClassicPopup()
        {
            goalText.DOFade(1, 0.25f).OnComplete(() =>
            {
                goalText.DOFade(0, 0.25f).SetDelay(1f).OnComplete(() =>
                {
                    GameObjectsState(true);
                    LevelManager.Instance.CurrentLevel.StartLevel();
                });
            });
        }

        private void ViewBossPopup()
        {
            bossPopup.Open();
            goalText.DOFade(0, 3).OnComplete(() =>
            {
                bossPopup.Close();
                GameObjectsState(true);
                LevelManager.Instance.CurrentLevel.StartLevel();
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