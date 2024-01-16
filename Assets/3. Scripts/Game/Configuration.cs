﻿using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Game.Units.Scriptable;
using _3._Scripts.Game.Weapon.Scriptable;
using net.krej.Singleton;
using UnityEngine;
using YG;

namespace _3._Scripts.Game
{
    public class Configuration: Singleton<Configuration>
    {
        [SerializeField] private List<WeaponConfig> weaponConfigs;
        
        public List<WeaponConfig> WeaponConfigs => weaponConfigs;
    }
}