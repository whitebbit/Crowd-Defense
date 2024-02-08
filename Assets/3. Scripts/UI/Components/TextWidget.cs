using _3._Scripts.UI.Manager;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI.Components
{    
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextWidget: MonoBehaviour
    {
        protected TextMeshProUGUI Text;

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
        }
        
        protected abstract void OnChange(float oldValue, float newValue);
    }
}