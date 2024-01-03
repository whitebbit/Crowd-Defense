using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Config.Types
{
    [Serializable]
    public class ListVariable<T> : BaseVariable
    {
        [field: SerializeField] public List<T> Value { get; private set; }
    }
}