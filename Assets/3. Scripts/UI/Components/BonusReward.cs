using _3._Scripts.UI.Extensions;
using TMPro;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Components
{
    public class BonusReward : MonoBehaviour
    {
        [Header("Game")] [SerializeField] private float moveSpeed;
        [SerializeField] private RectTransform indicator;
        [SerializeField] private Transform[] pathPoints;
        [Header("Visual")]
        [SerializeField] private LangYGAdditionalText multiplierText;
        [SerializeField] private TextMeshProUGUI rewardText;

        private int _pathIndex;
        public bool Blocked { get; set; }
        public bool Used { get; set; }
        public UIBonusMultiplier CurrentMultiplier { get; private set; }

        private void Update()
        {
            MoveIndicator();
        }

        public void ResetBonus()
        {
            Used = false;
            Blocked = false;
        }
        
        private void MoveIndicator()
        {
            if (Blocked) return;

            var position = pathPoints[_pathIndex].position;

            var distance = Vector3.Distance(pathPoints[0].position, pathPoints[1].position);

            var deviceSpeed = YandexGame.EnvironmentData.isMobile ? moveSpeed / 4 : moveSpeed;
            var speed = distance / deviceSpeed;
            indicator.position = Vector3.MoveTowards(indicator.position, position, speed);

            SetMultiplier();

            if (!(Vector3.Distance(indicator.position, position) <= 0.1f)) return;

            _pathIndex += 1;
            if (_pathIndex >= pathPoints.Length)
                _pathIndex = 0;
        }
        
        private void SetMultiplier()
        {
            var bonusMultiplier = UIRaycast.FindObject<UIBonusMultiplier>(indicator.position);
            if (bonusMultiplier == null) return;

            multiplierText.additionalText = $"{bonusMultiplier.Multiplier}X ";
            //rewardText.text = $"+{50 * bonusMultiplier.Multiplier}";
            CurrentMultiplier = bonusMultiplier;
        }
        
    }
}