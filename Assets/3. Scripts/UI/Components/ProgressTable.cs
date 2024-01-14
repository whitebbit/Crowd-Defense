using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Components
{
    public class ProgressTable : MonoBehaviour
    {
        [Header("Boss")] [SerializeField] private Image bossIcon;
        [Header("Levels")]
        [SerializeField] private List<ProgressSection> levels = new();
        [Header("Colors")] [SerializeField] private Color emptyColor;
        [SerializeField] private Color completedColor;
        [SerializeField] private Color nextColor;


        public void SetBossIcon(Sprite icon) => bossIcon.sprite = icon;
        
        public void SetLevel(int completedLevelNumber)
        {
            if (completedLevelNumber >= levels.Count) return;

            foreach (var level in levels)
            {
                level.SetColor(emptyColor);
            }

            for (var i = 0; i < completedLevelNumber; i++)
            {
                levels[i].SetColor(completedColor);
            }

            if (completedLevelNumber == levels.Count) return;

            levels[completedLevelNumber].SetColor(nextColor);
        }
    }
}