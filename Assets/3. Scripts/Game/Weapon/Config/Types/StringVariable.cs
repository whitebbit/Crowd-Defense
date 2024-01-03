using System;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Config.Types
{
    [Serializable]
    public class StringVariable: BaseVariable
    {
        [field: SerializeField] public string Value { get; private set; }
    }
}