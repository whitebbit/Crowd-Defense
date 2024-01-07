using System;
using _3._Scripts.Extensions;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Float")] [SerializeField] private KeyValueHolder<float> floats;
        [Header("Integer")] [SerializeField] private KeyValueHolder<int> integers;
        [Header("Bool")] [SerializeField] private KeyValueHolder<bool> bools;


        public T Get<T>(string id)
        {
            if (typeof(T) == typeof(float))
            {
                if (floats.TryGetValue(id, out var value))
                    return (T)Convert.ChangeType(value, typeof(T));
            }
            else if (typeof(T) == typeof(int))
            {
                if (integers.TryGetValue(id, out var value))
                    return (T)Convert.ChangeType(value, typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                if (bools.TryGetValue(id, out var value))
                    return (T)Convert.ChangeType(value, typeof(T));
            }
            return default;
        }
    }
}