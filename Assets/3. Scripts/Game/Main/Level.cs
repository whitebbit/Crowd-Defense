using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Architecture;
using _3._Scripts.Game.AI;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

namespace _3._Scripts.Game.Main
{
    public class Level : Singleton<Level>
    {
        public int KillsCount { get; private set; }
        public bool LevelInProgress { get; private set; }
        public event Action<int> OnKill;

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

        public void CompleteLevel()
        {
            Debug.Log("Win");
        }

        public void KillBot()
        {
            KillsCount += 1;
            OnKill?.Invoke(KillsCount);
        }
    }
}