using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _3._Scripts.Extensions
{
    [Serializable]
    public class KeyValueHolder<T>
    {
        [field: SerializeField] public List<KeyValue<T>> Holder { get; private set; }

        public T GetValue(string key)
        {
            var item = Holder.FirstOrDefault(v => v.Key == key);
            return item != null ? item.Value : default;
        }
        
        public bool TryGetValue(string key, out T value)
        {
            var item = Holder.FirstOrDefault(v => v.Key == key);
            if (item != null)
            {
                value = item.Value;
                return true;
            }
            value = default;
            return false;
        }
    }
}