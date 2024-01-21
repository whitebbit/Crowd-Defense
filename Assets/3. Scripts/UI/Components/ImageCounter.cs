using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class ImageCounter: MonoBehaviour
    {
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Sprite filledSprite;
        [Space]
        [SerializeField] private Color emptyColor;
        [SerializeField] private Color filledColor;

        private List<Image> _images = new();

        private void Awake()
        {
            _images = new List<Image>(GetComponentsInChildren<Image>());
        }

        public void SetCount(int count)
        {
            foreach (var star in _images)
            {
                star.color = emptyColor;
                star.sprite = emptySprite;
            }

            for (var i = 0; i < count; i++)
            {
                _images[i].color = filledColor;
                _images[i].sprite = filledSprite;
            }
        }
    }
}