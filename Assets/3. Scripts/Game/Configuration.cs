using System.Collections.Generic;
using _3._Scripts.Game.Weapon.Scriptable;
using net.krej.Singleton;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class Configuration: Singleton<Configuration>
    {
        [SerializeField] private List<WeaponConfig> weaponConfigs;

        public List<WeaponConfig> WeaponConfigs => weaponConfigs;
    }
}