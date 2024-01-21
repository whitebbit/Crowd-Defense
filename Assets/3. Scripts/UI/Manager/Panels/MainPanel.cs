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
        [SerializeField] private Button shopButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private LangYGAdditionalText additional;
        [Space] [SerializeField] private RectTransform notification;
        private Tween _buttonTween;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            shopButton.onClick.AddListener(() => UIManager.Instance.CurrentState = UIState.Shop);
            settingsButton.onClick.AddListener(() => UIManager.Instance.CurrentState = UIState.Settings);
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            MainMenuEnvironment.Instance.EnvironmentState(true);
            ButtonAnimation();
            additional.additionalText = $" {YandexGame.savesData.completedLevelsCount}";
            notification.gameObject.SetActive(MoneyManager.MoneyCount >= 750);

            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            _buttonTween?.Pause();
            _buttonTween?.Kill();
            _buttonTween = null;
            startButton.transform.localScale = Vector3.one;
            base.Close(onComplete, duration);
        }

        private void ButtonAnimation()
        {
            _buttonTween = startButton.transform.DOScale(Vector3.one * 1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            _buttonTween.Play();
        }

        private void StartGame()
        {
            Transition.Instance.Close(0.3f).OnComplete(() =>
            {
                var i = YandexGame.savesData.currentLevel;
                var level = LevelManager.Instance.CreateLevel(i);

                MainMenuEnvironment.Instance.EnvironmentState(false);

                level.Player.SetCameraState(true);

                if (YandexGame.savesData.currentLevel % 4 == 0)
                {
                    Transition.Instance.Open(0.3f).SetDelay(0.3f);
                    UIManager.Instance.CurrentState = UIState.Roulette;
                }
                else
                {
                    UIManager.Instance.CurrentState = UIState.Play;
                }
            });
        }
    }
}