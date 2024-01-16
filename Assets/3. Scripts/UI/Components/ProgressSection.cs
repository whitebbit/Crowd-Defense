using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class ProgressSection: MonoBehaviour
    {
        private Image _image;
        private TextMeshProUGUI _text;
        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetTextColor(Color color)
        {
            _text.color = color;
        }
        
        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}