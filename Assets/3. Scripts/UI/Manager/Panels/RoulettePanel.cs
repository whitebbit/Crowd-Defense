using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Extensions;
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
using Transition = _3._Scripts.UI.Components.Transition;

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
        private List<WeaponRouletteItemConfig> _existedWeapons = new();
        private bool _rotating;
        private bool _rewardGot;
        private void Start()
        {
            getReward.onClick.AddListener(GetReward);
            rotateAgain.onClick.AddListener(() =>
            {
                if (_rotating) return;
                YandexGame.RewVideoShow(2);
            });
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent += OnReward;
            YandexGame.savesData.secondWeapon = "";
            YandexGame.SaveProgress();

            _rewardGot = false;
            
            InitializeExistedWeapons();
            AddRandomItems();

            base.Open(onComplete, duration);
            Rotate();
        }
        
        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent -= OnReward;

            ResetPanels();
            
            base.Close(onComplete, duration);
        }
        
        private void InitializeExistedWeapons()
        {
            var unlocked = YandexGame.savesData.unlockedWeapons;
            _existedWeapons = configs
                .Select(w => w as WeaponRouletteItemConfig)
                .Where(w => w != null)
                .Where(w => unlocked.Contains(w.WeaponID) && w.WeaponID != YandexGame.savesData.currentWeapon).ToList();
        }

        private void OnReward(int obj)
        {
            if (obj != 2) return;

            Rotate();
            rotateAgain.gameObject.SetActive(false);
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
            if(_rewardGot) return;
            
            _rewardGot = true;
            _currentReward.OnReward(() =>
            {
                StopAllCoroutines();
                StartCoroutine(DelayReward());
            });
        }

        private void AddRandomItems()
        {
            foreach (var item in items.Shuffle())
            {
                if (50f.DropChance() && _existedWeapons.Count > 0)
                    AddWeaponsToRewards(item);
                else
                    AddOtherRewards(item);
            }
        }

        private void AddWeaponsToRewards(RouletteItem item)
        {
            if (_existedWeapons.Count <= 0) return;

            var randWeapon = _existedWeapons[Random.Range(0, _existedWeapons.Count)];

            item.Initialize(randWeapon);
            _existedWeapons.Remove(randWeapon);
        }

        private void AddOtherRewards(RouletteItem item)
        {
            var other = configs.Where(obj => obj is not WeaponRouletteItemConfig).ToList();
            var randomReward = other[Random.Range(0, other.Count)];

            item.Initialize(randomReward);
        }

        private void ResetPanels()
        {
            foreach (var item in items)
            {
                item.Initialized = false;
            }

            rotateAgain.gameObject.SetActive(true);
            _existedWeapons.Clear();
        }
        
        private IEnumerator DelayReward()
        {
            yield return new WaitForSeconds(0.5f);
            Transition.Instance.Close(0.3f).OnComplete(() => { UIManager.Instance.CurrentState = UIState.Play; });
        }
    }
}