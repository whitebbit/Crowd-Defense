using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class ProgressTable : MonoBehaviour
    {
        [Header("Boss")] [SerializeField] private Slider slider;
        [Header("Levels")] [SerializeField] private List<ProgressSection> levels = new();
        [Header("Images")] [SerializeField] private Sprite completeImage;
        [SerializeField] private Sprite emptyImage;
        [Header("Colors")] [SerializeField] private Color completeColor;
        [SerializeField] private Color emptyColor;

        public void SetLevel(int completedLevelNumber)
        {
            foreach (var level in levels)
            {
                level.SetImage(emptyImage);
                level.SetTextColor(emptyColor);
            }

            slider.value = (completedLevelNumber - 1f) * 1f / levels.Count;

            switch (completedLevelNumber)
            {
                case 1:
                    levels[0].SetImage(completeImage);
                    levels[0].SetTextColor(completeColor);
                    slider.value = 0;
                    return;
                case 8:
                    slider.value = 1;
                    break;
            }

            if (completedLevelNumber >= levels.Count)
            {
                foreach (var level in levels)
                {
                    level.SetImage(completeImage);
                    level.SetTextColor(completeColor);
                }
                return;
            }
            
            for (var i = 0; i < completedLevelNumber; i++)
            {
                levels[i].SetImage(completeImage);
                levels[i].SetTextColor(completeColor);
            }
        }
    }
}