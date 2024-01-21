using System;
using _3._Scripts.UI.Manager;
using UnityEngine;

namespace _3._Scripts.UI.Components
{
    public class KeysWidget: MonoBehaviour
    {
        private ImageCounter _imageCounter;

        private void Awake()
        {
            _imageCounter = GetComponent<ImageCounter>();
        }

        private void OnEnable()
        {
            OnChange(KeysManager.KeysCount);
            KeysManager.OnChanged += OnChange;
        }
        
        private void OnDisable()
        {
            KeysManager.OnChanged -= OnChange;
        }
        
        private void OnChange(int newValue)
        {
            if(!_imageCounter.CanSet()) return;
            
            _imageCounter.SetCount(newValue);
        }
    }
}