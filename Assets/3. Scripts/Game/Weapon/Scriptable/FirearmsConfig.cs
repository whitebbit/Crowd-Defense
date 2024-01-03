using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Firearms", fileName = "FirearmsConfig")]
    public class FirearmsConfig: WeaponConfig
    {
        [SerializeField] private float reloadTime;
        [SerializeField] private int bulletCount;

        public int BulletCount => bulletCount;
        public float ReloadTime => reloadTime;
    }
}