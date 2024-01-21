using UnityEngine;

namespace _3._Scripts.Extensions
{
    public static class RandomExtensions
    {
        public static bool DropChance(this float percentage)
        {
            var randomValue = Random.Range(0f, 100f);
            return randomValue <= percentage;
        }
        public static bool DropChance(this int percentage)
        {
            var randomValue = Random.Range(0f, 100f);
            return randomValue <= percentage;
        }
    }
}