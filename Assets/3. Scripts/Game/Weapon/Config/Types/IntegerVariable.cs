using System;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Config.Types
{
    [Serializable]
    public class IntegerVariable: BaseVariable
    {
        [field: SerializeField] public int Value { get; private set; }
    }
}