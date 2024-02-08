using System;
using _3._Scripts.UI.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI.Components
{
    public class MoneyWidget: TextWidget
    {
        private void OnEnable()
        {
            OnChange(MoneyManager.MoneyCount, MoneyManager.MoneyCount);
            MoneyManager.OnChanged += OnChange;
        }

        private void OnDisable()
        {
            MoneyManager.OnChanged -= OnChange;
        }

        protected override void OnChange(float oldValue, float newValue)
        {
            Text.DOCounter((int)oldValue, (int)newValue, 0.15f);
        }
        
    }
}