using _3._Scripts.Game;
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
            progressTable.SetBossIcon(Configuration.Instance.CurrentBoss.Icon);
        }

        private void SetKillsCount()
        {
            killsCount.text = $"{LevelManager.Instance.CurrentLevel.KillsCount}/{LevelManager.Instance.CurrentLevel.BotsCount}";
        }

        private void Continue()
        {
            if(bonusReward.Blocked) return;
            
            bonusReward.Blocked = true;
            Transition.Instance.Close(0.3f);
            
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
            YandexGame.SaveProgress();
            
            levelNumberText.additionalText = $" {YandexGame.savesData.completedLevelsCount}";
        }
    }
}