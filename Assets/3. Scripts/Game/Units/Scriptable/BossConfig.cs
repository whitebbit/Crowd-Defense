using UnityEngine;

namespace _3._Scripts.Game.Units.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Units/Boss Config", fileName = "Boss Config")]
    public class BossConfig: ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite icon;

        public string ID => id;
        public Sprite Icon => icon;
    }
}