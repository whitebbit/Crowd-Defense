
using UnityEngine;


namespace _3._Scripts.Game.AI
{
    public class Bot: MonoBehaviour
    {
        private BotFSM _botFsm;
        private void Awake()
        {
            _botFsm = new BotFSM(transform);
        }

        private void Update()
        {
            _botFsm.Update();
        }

        private void FixedUpdate()
        {
            _botFsm.FixedUpdate();
        }
    }
}