using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        protected bool Opened;
        protected CanvasGroup CanvasGroup;
        private readonly List<Tween> _currentTween = new List<Tween>();

        protected virtual void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        public virtual void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            if (Opened)
                return;

            ClearTween();
        
            var tween = CanvasGroup.DOFade(1, duration)
                .OnComplete(() =>
                {
                    CanvasGroup.blocksRaycasts = true;
                    Opened = true;
                    onComplete?.Invoke();
                });
            
            AddTween(tween);
        }

        public virtual void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            if (!Opened)
                return;

            Opened = false;

            ClearTween();

            var tween = CanvasGroup.DOFade(0, duration)
                .OnComplete(() =>
                {
                    CanvasGroup.blocksRaycasts = false;
                    onComplete?.Invoke();
                    gameObject.SetActive(false);
                });

            AddTween(tween);
        }

        protected void ClearTween()
        {
            foreach (var tween in _currentTween)
            {
                tween.Kill();
            }

            _currentTween.Clear();
        }

        protected void AddTween(Tween tween) => _currentTween.Add(tween);
    }
}