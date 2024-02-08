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
        [SerializeField] private Image opener;
        
        private Vector3 _sizeOpener;

        private void Start()
        {
            var sizeDelta = GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            _sizeOpener = Screen.height > Screen.width
                ? new Vector2(sizeDelta.y, sizeDelta.y) * 3
                : new Vector2(sizeDelta.x, sizeDelta.y) * 3;
            
            opener.rectTransform.sizeDelta = openedOnStart ? _sizeOpener : Vector2.zero;
        }

        public Tween Open(float duration)
        {
            opener.rectTransform.sizeDelta = Vector2.zero;
            return opener.rectTransform.DOSizeDelta(_sizeOpener, duration);
        }

        public Tween Close(float duration)
        {
            opener.rectTransform.sizeDelta = _sizeOpener;
            return opener.rectTransform.DOSizeDelta(Vector2.zero, duration);
        }
    }
}