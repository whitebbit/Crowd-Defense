using System;
using _3._Scripts.UI.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI.Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyWidget: MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            OnChange(MoneyManager.MoneyCount, MoneyManager.MoneyCount);
        }

        private void OnEnable()
        {
            MoneyManager.OnChanged += OnChange;
        }

        private void OnDisable()
        {
            MoneyManager.OnChanged -= OnChange;
        }

        private void OnChange(int oldValue, int newValue)
        {
            _text.DOCounter(oldValue, newValue, 0.15f);
        }
        
    }
}