using System;
using DG.Tweening;
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
        [SerializeField] private Image selectionBorder;
        [SerializeField] private ImageCounter stars;
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
            stars.SetCount(level);
            if (Unlocked)
                Unlock();
        }

        public void AddListener(UnityAction<ShopItem> action) => _button.onClick.AddListener(()=> action?.Invoke(this));
        public void Unlock()
        {
            locker.gameObject.SetActive(false);
        }

        public void SelectionState(bool state) => selectionBorder.DOFade(state ? 1 : 0, 0.15f);
        public void Upgrade(int level)
        {
            stars.SetCount(level);
        }
    }
}