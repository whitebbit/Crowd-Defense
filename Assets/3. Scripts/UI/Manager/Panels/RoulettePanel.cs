using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using _3._Scripts.UI.Extensions;
using _3._Scripts.UI.Scriptable;
using DG.Tweening;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace _3._Scripts.UI.Manager.Panels
{
    public class RoulettePanel : UIPanel
    {
        [Header("Roulette")] [SerializeField] private Ease ease;
        [Space] [SerializeField] private RectTransform roulette;
        [SerializeField] private List<RouletteItem> items = new();
        [SerializeField] private List<RouletteItemConfig> configs = new();
        [Space] [SerializeField] private RectTransform detectObject;
        [Header("Buttons")] [SerializeField] private Button getReward;
        [SerializeField] private Button rotateAgain;

        private RouletteItem _currentReward;
        private bool _rotating;

        private void Start()
        {
            getReward.onClick.AddListener(GetReward);
            rotateAgain.onClick.AddListener(() => YandexGame.RewVideoShow(2));
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent += OnReward;

            YandexGame.savesData.secondWeapon = "";
            YandexGame.SaveProgress();
            
            TryAddWeaponsToRewards();
            AddOtherRewards();

            base.Open(onComplete, duration);
            Rotate();
        }

        private void OnReward(int obj)
        {
            if (obj != 2) return;

            Rotate();
            rotateAgain.gameObject.SetActive(false);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent -= OnReward;

            ResetPanels();
            base.Close(onComplete, duration);
        }

        private void Rotate()
        {
            var rand = Random.Range(0, items.Count);
            var z = -45 * rand;
            var vector = new Vector3(0, 0, z - 720 - 720 - 720);

            _rotating = true;
            roulette.DORotate(vector, 5, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(ease)
                .SetDelay(0.4f)
                .OnComplete(() =>
                {
                    _rotating = false;
                    _currentReward = UIRaycast.FindObject<RouletteItem>(detectObject.position);
                });
        }

        private void GetReward()
        {
            if (_rotating) return;

            _currentReward.OnReward(() =>
            {
                StopAllCoroutines();
                StartCoroutine(DelayReward());
            });
        }

        private void TryAddWeaponsToRewards()
        {
            var unlocked = YandexGame.savesData.unlockedWeapons;
            var weapons = configs.Select(c => c as WeaponRouletteItemConfig)
                .Where(weapon => unlocked.Exists(u => weapon != null && u == weapon.WeaponID)).ToList();
            foreach (var weapon in weapons)
            {
                items.Find(i => !i.Initialized).Initialize(weapon);
            }
        }

        private void AddOtherRewards()
        {
            var other = configs.Where(obj => obj is not WeaponRouletteItemConfig).ToList();
            var notInitialized = items.Where(i => !i.Initialized).ToList();

            foreach (var item in notInitialized)
            {
                var randomReward = other[Random.Range(0, other.Count)];
                item.Initialize(randomReward);
            }
        }

        private void ResetPanels()
        {
            foreach (var item in items)
            {
                item.Initialized = false;
            }

            rotateAgain.gameObject.SetActive(true);
        }

        private IEnumerator DelayReward()
        {
            yield return new WaitForSeconds(0.5f);
            UIManager.Instance.CurrentState = UIState.Main;
        }
    }
}