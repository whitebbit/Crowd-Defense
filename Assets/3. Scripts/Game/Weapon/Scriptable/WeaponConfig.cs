using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Game.Weapon.Config.Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Weapon.Scriptable
{    
    [CreateAssetMenu(menuName = "Configs/Weapons", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Header("Float")]
        [SerializeField] private List<FloatVariable> floatVariables = new List<FloatVariable>();
        [Header("Integer")]
        [SerializeField] private List<IntegerVariable> integerVariables = new List<IntegerVariable>();
        [Header("String")]
        [SerializeField] private List<StringVariable> stringVariables = new List<StringVariable>();
        
        public float GetFloat(string id)
        {
            return floatVariables.First(f => f.ID == id).Value;
        }
        
        public int GetInteger(string id)
        {
            return integerVariables.First(f => f.ID == id).Value;
        }
        
        public string GetString(string id)
        {
            return stringVariables.First(f => f.ID == id).Value;
        }
    }
}