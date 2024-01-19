using System;
using _3._Scripts.Game;
using _3._Scripts.UI.Components;
using _3._Scripts.UI.Enums;
using DG.Tweening;
using TMPro;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Manager.Panels
{
    public class ShopPanel : UIPanel
    {
        [SerializeField] private ShopItem item;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button back;
        [Header("Coins buy")] [SerializeField] private Button coinsBuy;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        [SerializeField] private TextMeshProUGUI coinsBuyText;
        [SerializeField] private TextMeshProUGUI coinsUpgradeText;
        [Header("AD buy")] [SerializeField] private Button adBuy;
        [SerializeField] private TextMeshProUGUI adBuyText;
        [SerializeField] private TextMeshProUGUI adUpgradeText;
        [Header("Select")] [SerializeField] private Image weaponIcon;
        [Space] [SerializeField] private Button selectButton;
        [SerializeField] private TextMeshProUGUI selectedText;
        [SerializeField] private TextMeshProUGUI selectText;

        [Header("Price")] [SerializeField] private int buyPrice;
        [SerializeField] private SerializableDictionary<int, int> upgradePrice;

        private ShopItem _currentItem;

        private void Start()
        {
            InitializeItems();

            back.onClick.AddListener(() => UIManager.Instance.CurrentState = UIState.Main);
            coinsBuy.onClick.AddListener(CoinsBuy);
            selectButton.onClick.AddListener(SelectWeapon);
            adBuy.onClick.AddListener(() => YandexGame.RewVideoShow(3));
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent += AdBuy;
            base.Open(onComplete, duration);
        }

        public override void Close(TweenCallback onComplete = null, float duration = 0.3f)
        {
            YandexGame.RewardVideoEvent -= AdBuy;

            base.Close(onComplete, duration);
        }


        private void InitializeItems()
        {
            foreach (var config in Configuration.Instance.WeaponConfigs)
            {
                var obj = Instantiate(item, container);
                var visual = config.Visual;
                var id = config.Get<string>("id");
                var level = YandexGame.savesData.GetWeaponLevel(id);

                obj.Initialize(id, visual.GetTitle(YandexGame.lang), visual.Icon, level);
                obj.AddListener(SelectItem);

                if (id == YandexGame.savesData.currentWeapon)
                    SelectItem(obj);
            }
        }

        private void SelectItem(ShopItem shopItem)
        {
            _currentItem = shopItem;
            weaponIcon.sprite = _currentItem.Icon;
            UpdateButtons();
        }

        private void CoinsBuy()
        {
            if (!_currentItem.Unlocked)
                Buy(true);
            else
                Upgrade(true);

            UpdateButtons();
        }

        private void AdBuy(int obj)
        {
            if (obj != 3) return;

            if (!_currentItem.Unlocked)
                Buy(false);
            else
                Upgrade(false);

            UpdateButtons();
        }

        private void Buy(bool byCoins)
        {
            if (byCoins)
                MoneyManager.MoneyCount -= buyPrice;

            _currentItem.Unlock();

            YandexGame.savesData.unlockedWeapons.Add(_currentItem.ID);
            YandexGame.SaveProgress();
        }

        private void Upgrade(bool byCoins)
        {
            var currentLevel = YandexGame.savesData.GetWeaponLevel(_currentItem.ID);
            var price = upgradePrice[currentLevel];
            if (byCoins)
                MoneyManager.MoneyCount -= price;

            _currentItem.Upgrade(currentLevel + 1);
            YandexGame.savesData.weaponsLevel[_currentItem.ID] = currentLevel + 1;
            YandexGame.SaveProgress();
        }

        private void SelectWeapon()
        {
            YandexGame.savesData.currentWeapon = _currentItem.ID;
            YandexGame.SaveProgress();

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            UpdateCoinButton();
            UpdateSelectButton();
            UpdateAdButton();
        }

        private void UpdateCoinButton()
        {
            var currentLevel = YandexGame.savesData.GetWeaponLevel(_currentItem.ID);
            if (currentLevel >= 3)
            {
                coinsBuy.gameObject.SetActive(false);
                return;
            }

            var price = _currentItem.Unlocked ? upgradePrice[currentLevel] : buyPrice;
            coinsBuy.gameObject.SetActive(true);
            coinsBuyText.gameObject.SetActive(!_currentItem.Unlocked);
            coinsUpgradeText.gameObject.SetActive(_currentItem.Unlocked);
            coinsPrice.text = $"<sprite=0>{price}";
        }

        private void UpdateAdButton()
        {
            var currentLevel = YandexGame.savesData.GetWeaponLevel(_currentItem.ID);
            if (currentLevel >= 3)
            {
                adBuy.gameObject.SetActive(false);
                return;
            }

            adBuy.gameObject.SetActive(true);
            adBuyText.gameObject.SetActive(!_currentItem.Unlocked);
            adUpgradeText.gameObject.SetActive(_currentItem.Unlocked);
        }

        private void UpdateSelectButton()
        {
            if (!_currentItem.Unlocked)
            {
                selectButton.gameObject.SetActive(false);
                return;
            }

            var selected = YandexGame.savesData.currentWeapon == _currentItem.ID;
            selectButton.gameObject.SetActive(true);
            selectText.gameObject.SetActive(!selected);
            selectedText.gameObject.SetActive(selected);
        }
    }
}