using System;
using _3._Scripts.Architecture;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class Transition : Singleton<Transition>
    {
        [SerializeField] private bool openedOnStart = true;
        [Space] [SerializeField] private Image background;
        [SerializeField] private Image opener;
        [Space] [SerializeField] private CanvasScaler canvasScaler;

        private float Height => Screen.height * canvasScaler.scaleFactor;
        private float Width => Screen.width * canvasScaler.scaleFactor;

        private Vector2 Size =>
            Width > Height ? new Vector2(Width * 4, Width * 4) : new Vector2(Height * 2.5f, Height * 2.5f);

        private RectTransform OpenerTransform => opener.transform as RectTransform;
        private RectTransform BackgroundTransform => background.transform as RectTransform;
        
        private void Start()
        {
            OpenerTransform.sizeDelta = openedOnStart ? Size : Vector2.zero;
        }

        private void OnEnable()
        {
            SetBackgroundSize();
        }

        public Tween Open(float duration)
        {
            OpenerTransform.sizeDelta = Vector2.zero;
            return OpenerTransform.DOSizeDelta(Size, duration);
        }

        public Tween Close(float duration)
        {
            OpenerTransform.sizeDelta = Size;
            return OpenerTransform.DOSizeDelta(Vector2.zero, duration);
        }

        private void SetBackgroundSize()
        {
            var size = Width > Height ? 1.8f : 1f;
            BackgroundTransform.sizeDelta = new Vector2(Width * size, Height * size);
        }
    }
}