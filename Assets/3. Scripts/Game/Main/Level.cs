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
        public int KillsCount { get; private set; }
        public bool LevelInProgress { get; private set; }
        public int BotsCount { get; private set; }
        private int _attackedBotCount;

        public event Action<int> OnKill;
        public List<Bot> Bots { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            Player = GetComponentInChildren<Player>();
            Bots = new List<Bot>(transform.GetComponentsInChildren<Bot>());
            BotsCount = Bots.Count;
        }

        private void Start()
        {
        }

        public void StartLevel()
        {
            Player.SelectAdditionalWeapon(YandexGame.savesData.secondWeapon);
            LevelInProgress = true;
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

            if (NoMoreBots())
                CompleteLevel();
        }

        public void BotAttacked()
        {
            _attackedBotCount += 1;
            
            if (NoMoreBots())
                CompleteLevel();
        }

        private bool NoMoreBots()
        {
            return BotsCount - KillsCount - _attackedBotCount <= 0;
        }

        private IEnumerator DelayComplete()
        {
            if (HealthManager.HealthCount <= 0) yield break;

            LevelInProgress = false;
            
            yield return new WaitForSeconds(1f);
            
            if (HealthManager.HealthCount <= 0) yield break;
            
            UIManager.Instance.CurrentState = KeysManager.KeysCount != 3 ? UIState.Win : UIState.Chest;
        }
    }
}