using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class Timer
    {
        private readonly Image _timerImage;
        private readonly TextMeshProUGUI _timerText;
        private event Action OnTime;
        private bool _completed;
        private readonly List<Tween> _tweens = new();
        
        public Timer(Image image = null, TextMeshProUGUI text = null, Action onTime = null)
        {
            _timerImage = image;
            _timerText = text;
            OnTime = onTime;
        }

        public void StartTimer(int time)
        {
            _completed = false;
            
            ImageTimer(time);

            TextTimer(time);
        }

        public void StopTimer()
        {
            foreach (var tween in _tweens)
            {
                tween.Pause();
                tween.Kill();
            }
            _tweens.Clear();
        }
        
        private void ImageTimer(int time)
        {
            if (_timerImage == null) return;
            
            _timerImage.fillAmount = 1;
            var tween = _timerImage.DOFillAmount(0, time)
                .SetEase(Ease.Linear)
                .OnComplete(OnComplete);
            
            _tweens.Add(tween);
        }

        private void TextTimer(int time)
        {
            if (_timerText == null) return;
            
            _timerText.text = $"{time}";
            var tween =_timerText.DOCounter(time, 0, time)
                .SetEase(Ease.Linear)
                .OnComplete(OnComplete);
            
            _tweens.Add(tween);
        }

        private void OnComplete()
        {
            if(_completed) return;
            
            OnTime?.Invoke();
            _completed = true;
        }
    }
}