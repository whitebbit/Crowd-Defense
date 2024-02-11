using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI.Components
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> points = new();
        [SerializeField] private RectTransform hand;
        private CanvasGroup _canvasGroup;
        private Tween _tween;
        public event Action OnStop;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
                Stop();
        }

        public void StartTutorial()
        {
            gameObject.SetActive(true);
            
            var list = points.Select(t => t.transform.position).ToArray();
            hand.transform.position = list[0];
            
            _tween = hand.DOPath(list, 2, PathType.CatmullRom).SetLoops(-1, LoopType.Restart);
        }

        private void Stop()
        {
            if(_tween == null) return;
            
            _tween.Pause();
            _tween.Kill();
            _tween = null;
            
            _canvasGroup.DOFade(0, 0.25f).OnComplete(() => gameObject.SetActive(false));
            
            OnStop?.Invoke();
        }
    }
}