using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Architecture;
using _3._Scripts.Game.AI;
using _3._Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Main
{
    public class Level : Singleton<Level>
    {
        public int KillsCount { get; private set; }
        public event Action<int> OnKill;
        public bool LevelInProgress;
        private List<Bot> _bots = new();
        
        private void Awake()
        {
            _bots = new List<Bot>(transform.GetComponentsInChildren<Bot>());
        }

        private void Start()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            LevelInProgress = true;
        }
        
        public void KillBot()
        {
            KillsCount += 1;
            MoneyManager.MoneyCount += 1;
            OnKill?.Invoke(KillsCount);
        }
    }
}