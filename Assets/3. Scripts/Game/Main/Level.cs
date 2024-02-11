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
    public class Level : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves;

        public int KillsCount { get; private set; }
        public bool LevelInProgress { get; private set; }
        public bool LevelComplete { get; private set; }
        public int BotsCount { get; private set; }
        public Player Player { get; private set; }
        public Wave currentWave => waves[_currentWaveIndex];
        public event Action<int> OnKill;

        private int _attackedBotCount;
        private int _currentWaveIndex;

        private void Awake()
        {
            Player = GetComponentInChildren<Player>();
        }

        private void Start()
        {
            foreach (var wave in waves)
            {
                BotsCount += wave.BotsCount;
                wave.State(false);
            }
        }

        private void Update()
        {
            if (LevelInProgress)
                currentWave.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (LevelInProgress)
                currentWave.OnFixedUpdate();
        }

        public void StartLevel()
        {
            Player.SelectAdditionalWeapon(YandexGame.savesData.secondWeapon);
            LevelInProgress = true;
            currentWave.State(true);

            if (currentWave.NoMoreBots())
                NextWave();
        }

        public void CompleteLevel()
        {
            if (!LevelInProgress) return;

            if (HealthManager.HealthCount <= 0) return;

            StartCoroutine(DelayComplete());
        }

        public void LoseLevel()
        {
            LevelInProgress = false;
        }

        public void KillBot()
        {
            KillsCount += 1;
            OnKill?.Invoke(KillsCount);
            currentWave.RemoveBot();

            if (currentWave.NoMoreBots())
                NextWave();
        }

        public void BotAttacked()
        {
            _attackedBotCount += 1;
            currentWave.RemoveBot();

            if (currentWave.NoMoreBots())
                NextWave();
        }

        private void NextWave()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex >= waves.Count)
            {
                CompleteLevel();
                return;
            }

            currentWave.State(true);
        }

        private bool NoMoreBots()
        {
            return BotsCount - KillsCount - _attackedBotCount <= 0;
        }

        private IEnumerator DelayComplete()
        {
            LevelInProgress = false;
            LevelComplete = true;
            yield return new WaitForSeconds(1f);

            UIManager.Instance.CurrentState = KeysManager.KeysCount != 3 ? UIState.Win : UIState.Chest;
        }
    }
}