using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Game;
using _3._Scripts.UI.Scriptable;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class Chest : MonoBehaviour
    {
        [Header("Chest")] [SerializeField] private Image chestIcon;
        [Header("Reward")] [SerializeField] private CanvasGroup rewardGroup;
        [SerializeField] private Image rewardIcon;
        [SerializeField] private TextMeshProUGUI rewardTitle;

        [Header("Getter Effect")] [SerializeField]
        private RectTransform heart;
        [SerializeField] private RectTransform coins;
        
        private Button _button;
        private RouletteItemConfig _reward;
        private FuncPredicate _predicate;
        private GetterEffect _getterEffect;
        private bool _unlocked;
        private void Awake()
        {
            _getterEffect = GetComponent<GetterEffect>();
            _button = GetComponent<Button>();
        }

        public void Initialize(RouletteItemConfig reward)
        {
            _reward = reward;

            rewardIcon.sprite = reward.Icon;
            rewardTitle.text = reward.Title;

            _button.onClick.AddListener(Open);
        }

        public void Resetting()
        {
            chestIcon.transform.localScale = Vector3.one;
            rewardGroup.alpha = 0;
            _reward = null;
            _predicate = null;
            _unlocked = false;
            _button.onClick.RemoveAllListeners();
        }

        public void AddListener(UnityAction action) => _button.onClick.AddListener(action);
        
        public void SetPredicate(FuncPredicate predicate) => _predicate = predicate;
        
        private void Open()
        {
            if (_unlocked) return;
            
            if(!_predicate.Evaluate()) return;
            
            AudioManager.Instance.PlayOneShot("reward");
            
            _unlocked = true;
            chestIcon.DOFade(0, 0.25f);
            chestIcon.transform.DOScale(Vector3.one * 1.5f, 0.25f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => rewardGroup.DOFade(1, 0.25f)
                    .OnComplete(() => DoEffect(_reward.GetReward)));

        }
        
        private void DoEffect(Action onComplete)
        {
            if (_reward is WeaponUnlockerItemConfig)
            {
                onComplete?.Invoke();
                return;
            }

            switch (_reward)
            {
                case HealthRouletteItemConfig:
                    _getterEffect.SetFinishPoint(heart);
                    break;
                case CoinsRouletteItemConfig:
                    _getterEffect.SetFinishPoint(coins);
                    break;
            }
            
            _getterEffect.DoMoneyEffect(5, onComplete);
        }
    }
}