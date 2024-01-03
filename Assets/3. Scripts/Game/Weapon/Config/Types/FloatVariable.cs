using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Weapon.Config.Types
{
    [Serializable]
    public class FloatVariable: BaseVariable
    {
        [field: SerializeField] public float Value { get; private set; }
    }
}