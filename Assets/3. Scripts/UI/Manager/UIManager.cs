using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using _3._Scripts.Architecture;
using _3._Scripts.UI.Enums;
using DG.Tweening;
using UI.Panels;
using UnityEngine;

namespace _3._Scripts.UI.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private UIState startState;
        [Space] 
        [SerializeField] private UIPanel mainPanel;
        [SerializeField] private UIPanel playPanel;
        [SerializeField] private UIPanel winPanel;
        [SerializeField] private UIPanel roulettePanel;
        [SerializeField] private UIPanel shopPanel;
        [SerializeField] private UIPanel settingsPanel;
        private bool _onTransition;

        public UIState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState == value) return;

                if (_onTransition) return;

                if (!Enum.IsDefined(typeof(UIState), value))
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(UIState));

                _onTransition = true;
                var newPanel = _panels[value];
                if (_currentState == UIState.None)
                {
                    OpenNewPanel(newPanel);
                }
                else
                {
                    CloseCurrentPanel(value, newPanel);
                }

                _currentState = value;
            }
        }

        private readonly Dictionary<UIState, UIPanel> _panels = new Dictionary<UIState, UIPanel>();
        private UIState _currentState;

        protected override void OnAwake()
        {
            _currentState = UIState.None;
            
            _panels.Add(UIState.Main, mainPanel);
            _panels.Add(UIState.Play, playPanel);
            _panels.Add(UIState.Win, winPanel);
            _panels.Add(UIState.Roulette, roulettePanel);
            _panels.Add(UIState.Shop, shopPanel);
            _panels.Add(UIState.Settings, settingsPanel);
        }

        private void Start()
        {
            Initialize();
        }
        
        public T GetPanel<T>() where T : UIPanel
        {
            return _panels.Values.First(p=> p is T) as T;
        }

        private void Initialize()
        {
            foreach (var panel in _panels.Values)
            {
                panel.Initialize();
            }

            CurrentState = startState;
        }

        private void CloseCurrentPanel(UIState value, UIPanel newPanel)
        {
            var currentPanel = _panels[_currentState];

            TweenCallback callback = value == UIState.None ? null : () => OpenNewPanel(newPanel);

            newPanel.gameObject.SetActive(true);
            currentPanel.Close(() => { callback?.Invoke(); });
        }

        private void OpenNewPanel(UIPanel newPanel)
        {
            newPanel.gameObject.SetActive(true);
            newPanel.Open(() => { _onTransition = false; });
        }
    }
}