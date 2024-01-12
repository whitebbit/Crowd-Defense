using System;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Float")] [SerializeField] private SerializableDictionary<string, float> floats;
        [Header("Integer")] [SerializeField] private SerializableDictionary<string, int> integers;
        [Header("Bool")] [SerializeField] private SerializableDictionary<string, bool> bools;
        [Header("Bool")] [SerializeField] private SerializableDictionary<string, string> strings;
        [Header("LayerMask")] [SerializeField] private SerializableDictionary<string, LayerMask> layerMasks;


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
            else if (typeof(T) == typeof(LayerMask))
            {
                if (layerMasks.TryGetValue(id, out var value))
                    return (T)Convert.ChangeType(value, typeof(T));
            }
            else if (typeof(T) == typeof(string))
            {
                if (strings.TryGetValue(id, out var value))
                    return (T)Convert.ChangeType(value, typeof(T));
            }
            
            throw new Exception($"{typeof(T)} object with id '{id}' not found");
        }
    }
}