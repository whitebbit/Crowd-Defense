using System;
using _3._Scripts.UI.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class RouletteItem : MonoBehaviour
    {
        [SerializeField] private RectTransform heart;
        [SerializeField] private RectTransform money;
        private Image _icon;
        private TextMeshProUGUI _text;
        private RouletteItemConfig _config;
        private GetterEffect _getterEffect;
        public bool Initialized { get; set; }
        
        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _getterEffect = GetComponent<GetterEffect>();
        }

        public void Initialize(RouletteItemConfig config)
        {
            Initialized = true;
            _config = config;
            _icon.sprite = config.Icon;
            _text.text = config.Title;
        }

        public void OnReward(Action onComplete)
        {
            DoEffect(() =>
            {
                onComplete?.Invoke();
                _config.GetReward();
            });
        }

        private void DoEffect(Action onComplete)
        {
            if (_config is WeaponRouletteItemConfig)
            {
                onComplete?.Invoke();
                return;
            }

            switch (_config)
            {
                case HealthRouletteItemConfig:
                    _getterEffect.SetFinishPoint(heart);
                    break;
                case CoinsRouletteItemConfig:
                    _getterEffect.SetFinishPoint(money);
                    break;
            }
            
            _getterEffect.DoMoneyEffect(5, onComplete);
        }
    }
}