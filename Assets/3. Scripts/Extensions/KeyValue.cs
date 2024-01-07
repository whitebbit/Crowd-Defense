using System;
using UnityEngine;

namespace _3._Scripts.Extensions
{
    [Serializable]
    public class KeyValue<T>
    {
        [field: SerializeField] public string Key { get; private set; }
        [field: SerializeField] public T Value { get; private set; }
    }
}