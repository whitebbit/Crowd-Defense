using System.Collections;
using System.Collections.Generic;
using _3._Scripts.Game;
using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
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
    public class WinPanel : UIPanel
    {
        [Header("Stats")] [SerializeField] private LangYGAdditionalText levelNumberText;
        [SerializeField] private TextMeshProUGUI killsCount;

        [Header("Additional")] [SerializeField]
        private ProgressTable progressTable;

        [SerializeField] private GetterEffect getterEffect;
        [SerializeField] private VictoryPopup victoryPopup;
        [SerializeField] private BonusReward bonusReward;
        [Space] [SerializeField] private RectTransform notification;
        [Header("Buttons")] [SerializeField] private Button getBonusButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button menuButton;

        protected override void Awake()
        {
            base.Awake();
            getBonusButton.onClick.AddListener(GetBonus);
            continueButton.onClick.AddListener(Continue);
            menuButton.onClick.AddListener(GoToMenu);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            _onContinue = false;

            SetKillsCount();
            InitializeProgress();
            SetLevelNumber();
            victoryPopup.Open();
            notification.gameObject.SetActive(MoneyManager.MoneyCount >= 750);
            YandexGame.RewardVideoEvent += OnReward;
            
            AudioManager.Instance.PlayOneShot("win_voice");
            AudioManager.Instance.PlayOneShot("win");
            
            base.Open(() =>
            {
                onComplete?.Invoke();
            }, duration);
        }


        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent -= OnReward;
            bonusReward.ResetBonus();
            victoryPopup.Close();

            base.Close(onComplete, duration);
        }

        private void InitializeProgress()
        {
            progressTable.SetLevel(YandexGame.savesData.currentLevel);
        }

        private void SetKillsCount()
        {
            killsCount.text =
                $"{LevelManager.Instance.CurrentLevel.KillsCount}/{LevelManager.Instance.CurrentLevel.BotsCount}";
        }

        private bool _onContinue;
        private void Continue()
        {
            if (_onContinue) return;

            _onContinue = true;
            bonusReward.Blocked = true;
            GetReward();
        }

        private void GetBonus()
        {
            if (bonusReward.Blocked) return;

            bonusReward.Used = true;
            bonusReward.Blocked = true;
            YandexGame.RewVideoShow(1);
        }

        private void GoToMenu()
        {
            if (bonusReward.Blocked) return;

            var count = bonusReward.Used ? 20 : 10;

            bonusReward.Blocked = true;
            getterEffect.DoMoneyEffect(count, () =>
            {
                MoneyManager.MoneyCount += bonusReward.Used ? bonusReward.CurrentMultiplier.Multiplier * 50 : 50;
                Transition.Instance.Close(0.3f).OnComplete(() =>
                {
                    LevelManager.Instance.DeleteLevel();
                    MainMenuEnvironment.Instance.EnvironmentState(true);
                    UIManager.Instance.CurrentState = UIState.Main;
                    
                    Transition.Instance.Open(0.3f).SetDelay(0.25f);
                }).SetDelay(0.25f);
            });
        }

        private void SetLevelNumber()
        {
            YandexGame.savesData.completedLevelsCount += 1;
            YandexGame.savesData.currentLevel += 1;
            YandexGame.SaveProgress();

            levelNumberText.additionalText = $" {YandexGame.savesData.completedLevelsCount}";
        }

        private void OnReward(int id)
        {
            if (id != 1) return;
            
            AudioManager.Instance.PlayOneShot("reward");
            bonusReward.SetReward();
        }

        private void GetReward()
        {
            var count = bonusReward.Used ? 20 : 10;
            getterEffect.DoMoneyEffect(count,
                () =>
                {
                    MoneyManager.MoneyCount += bonusReward.Used ? bonusReward.CurrentMultiplier.Multiplier * 25 : 25;
                    AudioManager.Instance.PlayOneShot("coins");
                    Transition.Instance.Close(0.3f).SetDelay(0.75f).OnComplete(() =>
                    {
                        LevelManager.Instance.DeleteLevel();
                        LevelManager.Instance.CreateLevel(YandexGame.savesData.currentLevel);
                        if (YandexGame.savesData.currentLevel % 2 == 0)
                        {
                            Transition.Instance.Open(0.3f).SetDelay(0.3f);
                            UIManager.Instance.CurrentState = UIState.Roulette;
                        }
                        else
                        {
                            UIManager.Instance.CurrentState = UIState.Play;
                        }
                    });
                });
        }
    }
}