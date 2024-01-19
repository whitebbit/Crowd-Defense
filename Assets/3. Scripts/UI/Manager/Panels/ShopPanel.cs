using System;
using _3._Scripts.Game;
using _3._Scripts.UI.Components;
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
        [Header("Buttons")] [SerializeField] private Button coinsBuy;
        [SerializeField] private TextMeshProUGUI coinsPrice;
        [SerializeField] private TextMeshProUGUI coinsBuyText;
        [SerializeField] private TextMeshProUGUI coinsUpgradeText;
        [Space]
        [SerializeField] private Button adBuy;
        [SerializeField] private TextMeshProUGUI adBuyText;
        [SerializeField] private TextMeshProUGUI adUpgradeText;
        [Header("Price")] [SerializeField] private int buyPrice;
        [SerializeField] private SerializableDictionary<int, int> upgradePrice;
        
        private ShopItem _currentItem;
        
        private void Start()
        {
            coinsBuy.onClick.AddListener(CoinsBuy);
            adBuy.onClick.AddListener(() => YandexGame.RewVideoShow(3));
        }

        public override void Open(TweenCallback onComplete = null, float duration = 0.3f)
        {
            InitializeItems();
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
            var i = 0;
            foreach (var config in Configuration.Instance.WeaponConfigs)
            {
                var obj = Instantiate(item, container);
                var visual = config.Visual;
                var id = config.Get<string>("id");
                var level = YandexGame.savesData.GetWeaponLevel(id);
                
                obj.Initialize(id, visual.GetTitle(YandexGame.lang), visual.Icon, level);

                if (i == 0)
                    SelectItem(obj);
                
                i++;
            }
        }

        private void SelectItem(ShopItem shopItem)
        {
            _currentItem = shopItem;
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

            YandexGame.savesData.unlockedWeapons.Add(_currentItem.ID);
            YandexGame.SaveProgress();
        }

        private void Upgrade(bool byCoins)
        {
            var currentLevel = YandexGame.savesData.GetWeaponLevel(_currentItem.ID);
            var price = upgradePrice[currentLevel];

            /*if (byCoins)
                MoneyManager.MoneyCount -= price;*/

            YandexGame.savesData.weaponsLevel[_currentItem.ID] = currentLevel + 1;
            YandexGame.SaveProgress();
        }
        
        private void UpdateButtons()
        {
            SetCoinButton();
            
            adBuyText.gameObject.SetActive(!_currentItem.Unlocked);
            adUpgradeText.gameObject.SetActive(_currentItem.Unlocked);
        }

        private void SetCoinButton()
        {
            var currentLevel = YandexGame.savesData.GetWeaponLevel(_currentItem.ID);
            
            var price = upgradePrice[currentLevel];
            
            coinsBuyText.gameObject.SetActive(!_currentItem.Unlocked);
            coinsUpgradeText.gameObject.SetActive(_currentItem.Unlocked);
            
            coinsPrice.text = $"<sprite=0>{price}";
        }
    }
}