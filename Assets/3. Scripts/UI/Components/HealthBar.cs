using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image bar;

        private void Awake()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }

        public void Change(float current, float max)
        {
            var value = current / max;
            
            bar.DOFillAmount(value, 0.1f).OnComplete(() =>
            {
                if (value <= 0)
                    gameObject.SetActive(false);
            });
        }
    }
}