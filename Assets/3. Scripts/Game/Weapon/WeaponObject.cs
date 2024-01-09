using UnityEngine;

namespace _3._Scripts.Game.Weapon
{
    public class WeaponObject: MonoBehaviour
    {
        [SerializeField] private Transform point;

        public Transform Point => point;
    }
}