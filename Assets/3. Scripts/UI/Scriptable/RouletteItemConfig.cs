using UnityEngine;

namespace _3._Scripts.UI.Scriptable
{
    public abstract class RouletteItemConfig: ScriptableObject
    {
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string title;

        public Sprite Icon => icon;
        public string Title => title;
        
        public abstract void GetReward();
    }
}