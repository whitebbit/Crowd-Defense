using _3._Scripts.Game.Main;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Manager.Panels
{
    public class MainPanel : UIPanel
    {
        [SerializeField] private Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            MainMenuEnvironment.Instance.EnvironmentState(true);
            base.Open(onComplete, duration);
        }

        private void StartGame()
        {
            Transition.Instance.Close(0.3f).OnComplete(() =>
            {
                var i = YandexGame.savesData.currentLevel;
                var level = LevelManager.Instance.CreateLevel(i);

                MainMenuEnvironment.Instance.EnvironmentState(false);
                
                level.Player.SetCameraState(true);

                UIManager.Instance.CurrentState = UIState.Play;
            });
        }
    }
}