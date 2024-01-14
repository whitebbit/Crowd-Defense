using System;
using _3._Scripts.Game;
using _3._Scripts.Game.Main;
using _3._Scripts.Game.Weapon.Scriptable;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Components
{
    public class WeaponSelector : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI bulletsCount;
        [Space] [SerializeField] private LevelStars levelStars;
        [Header("Colors")]
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        private bool _selected;
        private string _weaponId;
        public event Action<WeaponSelector> OnSelect; 
        
        private Button _button;
        private WeaponConfig _config;
        private Tween _reloadTween;
        private int BulletsCount => _config.Get<int>("bulletCount") +
                                    _config.Improvements.GetAmmoImprovement(_config.Get<string>("id"));

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Select);
            Unselect();
        }
        
        public void Initialize(string id)
        {
            _config = Configuration.Instance.WeaponConfigs.Find(w => w.Get<string>("id") == id); 
            if(_config == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            var level = YandexGame.savesData.GetWeaponLevel(id);
            var visual = _config.Visual;

            _weaponId = id;
            icon.sprite = visual.Icon;
            bulletsCount.text = $"{_config.Get<int>("bulletCount")}";
            levelStars.SetLevel(level);
            
            SubscribeToWeapon(id);
        }

        public void Select()
        {
            if(!gameObject.activeSelf) return;
            
            if(_selected) return;

            OnSelect?.Invoke(this);
            _button.image.DOColor(selectedColor, 0.25f);
            LevelManager.Instance.CurrentLevel.Player.SelectWeapon(_weaponId);
            _reloadTween?.Play();
            _selected = true;
        }

        public void Unselect()
        {
            if(!gameObject.activeSelf) return;
         
            if(!_selected) return;
            
            _button.image.DOColor(unselectedColor, 0.25f);
            _reloadTween.Pause();
            _selected = false;
        }

        private void SubscribeToWeapon(string id)
        {
            var weapon = LevelManager.Instance.CurrentLevel.Player.GetWeapon(id).WeaponFsm;
            weapon.onAttack += UpdateBulletsCount;
            weapon.onReloadStart += OnReload;
        }

        private void UpdateBulletsCount(int count)
        {
            bulletsCount.text = $"{count}";
            _button.image.fillAmount = count * 1f / BulletsCount;
        }
        
        private void OnReload(float time)
        {
            _reloadTween = _button.image.DOFillAmount(1, time).OnComplete(() =>
            {
                bulletsCount.DOCounter(0, BulletsCount, 0.1f);
            });
        }
    }
}