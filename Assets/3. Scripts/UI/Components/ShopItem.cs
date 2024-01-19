using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace _3._Scripts.UI.Components
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private LevelStars stars;
        [Space] [SerializeField] private RectTransform locker;

        public string ID { get; private set; }
        public Sprite Icon { get; private set; }
        public bool Unlocked => YandexGame.savesData.unlockedWeapons.Contains(ID);
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(string id, string titleText, Sprite iconImage, int level)
        {
            ID = id;
            Icon = iconImage;
            title.text = titleText;
            icon.sprite = iconImage;
            stars.SetLevel(level);
            if (Unlocked)
                Unlock();
        }

        public void AddListener(UnityAction<ShopItem> action) => _button.onClick.AddListener(()=> action?.Invoke(this));
        public void Unlock()
        {
            locker.gameObject.SetActive(false);
        }

        public void Upgrade(int level)
        {
            stars.SetLevel(level);
        }
    }
}