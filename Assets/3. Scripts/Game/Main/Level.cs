using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Architecture;
using _3._Scripts.Game.AI;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
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
        public int BotsCount { get; private set; }
        public event Action<int> OnKill;

        private void Awake()
        {
            BotsCount = new List<Bot>(transform.GetComponentsInChildren<Bot>()).Count;
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
            if (!LevelInProgress) return;

            StartCoroutine(DelayComplete());
        }

        public void KillBot()
        {
            KillsCount += 1;
            OnKill?.Invoke(KillsCount);
        }

        private IEnumerator DelayComplete()
        {
            LevelInProgress = false;
            yield return new WaitForSeconds(1f);
            UIManager.Instance.CurrentState = UIState.Win;
        }
    }
}