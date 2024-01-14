using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class LevelStars: MonoBehaviour
    {
        [SerializeField] private Color emptyColor;
        [SerializeField] private Color filledColor;

        private List<Image> _stars = new();

        private void Awake()
        {
            _stars = new List<Image>(GetComponentsInChildren<Image>());
        }

        public void SetLevel(int level)
        {
            foreach (var star in _stars)
            {
                star.color = emptyColor;
            }

            for (var i = 0; i < level; i++)
            {
                _stars[i].color = filledColor;
            }
        }
    }
}