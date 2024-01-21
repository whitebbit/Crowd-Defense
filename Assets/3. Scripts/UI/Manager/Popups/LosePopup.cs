using System;
using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Manager.Popups
{
    public class LosePopup: UIPanel
    {
        [SerializeField] private UIPanel popup;
        [SerializeField] private Button menu;
        [SerializeField] private Button addHealth;
        private void Start()
        {
            menu.onClick.AddListener(GoToMenu);
            addHealth.onClick.AddListener(ContinueGame);
            Initialize();
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            base.Open(onComplete, duration);
            popup.Open();
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            popup.Close();
            base.Close(onComplete, duration);
        }

        private void ContinueGame()
        {
            HealthManager.HealthCount += 50;
            LevelManager.Instance.CurrentLevel.StartLevel();
            Close();
        }
        
        private void GoToMenu()
        {
            MoneyManager.MoneyCount += 25;
            HealthManager.HealthCount = 100;
        
            Transition.Instance.Close(0.3f).OnComplete(() =>
            {
                LevelManager.Instance.DeleteLevel();
                MainMenuEnvironment.Instance.EnvironmentState(true);
                UIManager.Instance.CurrentState = UIState.Main;
                Transition.Instance.Open(0.3f).SetDelay(0.25f);
            }).SetDelay(0.25f);
        }
    }
}