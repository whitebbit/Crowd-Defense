using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class ProgressSection: MonoBehaviour
    {
        [SerializeField]private List<Image> images = new();

        public void SetColor(Color color)
        {
            foreach (var image in images)
            {
                image.color = color;
            }
        }
    }
}