using _3._Scripts.Architecture;
using Cinemachine;
using UnityEngine;

namespace _3._Scripts.Game.Main
{
    public class MainMenuEnvironment : Singleton<MainMenuEnvironment>
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform environment;

        public void EnvironmentState(bool state)
        {
            environment.gameObject.SetActive(state);
            virtualCamera.Priority = state ? 100 : -1;
        }
    }
}