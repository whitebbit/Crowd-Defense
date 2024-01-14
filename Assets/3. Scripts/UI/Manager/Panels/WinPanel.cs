using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using DG.Tweening;
using TMPro;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Manager.Panels
{
    public class WinPanel: UIPanel
    {
        [Header("Stats")]
        [SerializeField] private LangYGAdditionalText levelNumberText;
        [SerializeField] private TextMeshProUGUI killsCount;
        [Header("Additional")]
        [SerializeField] private ProgressTable progressTable;
        [SerializeField] private BonusReward bonusReward;
        [Header("Buttons")]
        [SerializeField] private Button getBonusButton;
        [SerializeField] private Button continueButton;
        protected override void Awake()
        {
            base.Awake();
            getBonusButton.onClick.AddListener(GetBonus);
            continueButton.onClick.AddListener(Continue);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            SetKillsCount();
            InitializeProgress();
            SetLevelNumber();
            base.Open(onComplete, duration);
        }
        
        private void InitializeProgress()
        {
            progressTable.SetLevel(YandexGame.savesData.currentLevel);
            progressTable.SetBossIcon(null);
        }

        private void SetKillsCount()
        {
            killsCount.text = $"{Level.Instance.KillsCount}/{Level.Instance.BotsCount}";
        }

        private void Continue()
        {
            bonusReward.Blocked = true;
            Transition.Instance.Close(0.25f).OnComplete(() =>
            {
                UIManager.Instance.CurrentState = UIState.Play;
                Transition.Instance.Open(0.5f);
            });
            //TODO: open main scene
        }
        
        private void GetBonus()
        {
            if(bonusReward.Blocked) return;
            
            bonusReward.Blocked = true;
            
            //TODO: open ad
        }

        private void SetLevelNumber()
        {
            YandexGame.savesData.completedLevelsCount += 1;
            YandexGame.savesData.currentLevel += 1;
            
            levelNumberText.additionalText = $" {YandexGame.savesData.completedLevelsCount}";
            
            YandexGame.SaveProgress();
        }
    }
}