using System;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Config.Types
{
    [Serializable]
    public abstract class BaseVariable
    {
        [field: SerializeField] public string ID { get; private set; }
    }
}