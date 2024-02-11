using System.Collections.Generic;
using _3._Scripts.Game.AI;
using UnityEngine;

namespace _3._Scripts.Game.Main
{
    public class Wave : MonoBehaviour
    {
        public List<Bot> Bots { get; private set; }
        public int BotsCount { get; private set; }

        private void Awake()
        {
            Bots = new List<Bot>(transform.GetComponentsInChildren<Bot>());
            BotsCount = Bots.Count;
        }

        public void State(bool state) => gameObject.SetActive(state);
        
        public void OnUpdate()
        {
            foreach (var bot in Bots)
            {
                bot.OnUpdate();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (var bot in Bots)
            {
                bot.OnFixedUpdate();
            }
        }

        public void RemoveBot() => BotsCount--;

        public bool NoMoreBots()
        {
            return BotsCount <=0 ;
        }
    }
}