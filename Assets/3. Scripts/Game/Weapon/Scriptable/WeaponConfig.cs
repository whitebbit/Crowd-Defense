using System;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Weapon.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Weapon Config", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private WeaponVisual weaponVisual;
        [SerializeField] private WeaponImprovements weaponImprovements;
        [Header("Parameters ")]
        [SerializeField] private SerializableDictionary<string, float> floats;
        [SerializeField] private SerializableDictionary<string, int> integers;
        [Space]
        [SerializeField] private SerializableDictionary<string, string> strings;
        [SerializeField] private SerializableDictionary<string, bool> bools;
        [Space]
        [SerializeField] private SerializableDictionary<string, LayerMask> layerMasks;

        public WeaponImprovements Improvements => weaponImprovements;
        public WeaponVisual Visual => weaponVisual;
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