using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Extensions;
using _3._Scripts.FSM.Base;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using _3._Scripts.UI.Scriptable;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI.Manager.Popups
{
    public class ChestPopup : UIPanel
    {
        [SerializeField] private RectTransform mainReward;
        [SerializeField] private Image mainRewardIcon;
        [Header("Buttons")]
        [SerializeField] private CanvasGroup buttons;
        [SerializeField] private Button next;
        [SerializeField] private Button moreKeys;
        [Header("Main")]
        [SerializeField] private ImageCounter keys;
        [Space]
        [SerializeField] private List<Chest> chests = new();
        [Header("Rewards")] [SerializeField] private List<RouletteItemConfig> commonRewards = new();
        [SerializeField] private List<WeaponUnlockerItemConfig> rareRewards = new();

        private bool _rareRewardUsed;
        private int _keysCount;
        private void Start()
        {
            Initialize();
            next.onClick.AddListener(() =>
            {
                Close(() =>
                {
                    UIManager.Instance.CurrentState = UIState.Win;
                });
            });
            moreKeys.onClick.AddListener(()=> YandexGame.RewVideoShow(4));
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            InitializePopup();
            InitializeChests();
            YandexGame.RewardVideoEvent += OnReward;
            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent -= OnReward;
            base.Close(onComplete, duration);
        }

        private void InitializePopup()
        {
            buttons.alpha = 0;
            
            mainReward.gameObject.SetActive(false);
            buttons.gameObject.SetActive(false);
            moreKeys.gameObject.SetActive(true);

            keys.SetCount(3);
            
            _rareRewardUsed = false;
            _keysCount = 3;
        }
        
        private RouletteItemConfig GetReward()
        {   
            if (!_rareRewardUsed && CanAddRare())
                return RareReward();
            
            return CommonReward();
        }
        
        private RouletteItemConfig RareReward()
        {
            var notExisted = rareRewards
                .Where(r => !YandexGame.savesData.unlockedWeapons.Contains(r.WeaponID)).ToList();
            var randomReward = notExisted[Random.Range(0, notExisted.Count)];
         
            _rareRewardUsed = true;
            mainReward.gameObject.SetActive(true);
            mainRewardIcon.sprite = randomReward.Icon;
            
            return randomReward;
        }

        private RouletteItemConfig CommonReward()
        {
            var randomReward = commonRewards[Random.Range(0, commonRewards.Count)];
            return randomReward;
        }
        
        private void InitializeChests()
        {
            
            foreach (var chest in chests)
            {
                chest.Resetting();
                chest.Initialize(GetReward());
                chest.AddListener(RemoveKey);
                chest.SetPredicate(new FuncPredicate(()=> _keysCount > 0));
            }
        }
        
        private bool CanAddRare()
        {
            return rareRewards.Any(r => !YandexGame.savesData.unlockedWeapons.Contains(r.WeaponID));
        }

        private void RemoveKey()
        {
            _keysCount--;
            keys.SetCount(_keysCount);
            if(_keysCount != 0) return;
            
            buttons.gameObject.SetActive(true);
            buttons.DOFade(1, 0.25f);
        }
        
        private void OnReward(int obj)
        {
            if(obj != 4) return;
            _keysCount = 3;
            keys.SetCount(_keysCount);
            buttons.gameObject.SetActive(false);
            moreKeys.gameObject.SetActive(false);
            buttons.alpha = 0;
        }
    }
}