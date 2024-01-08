using System.Collections.Generic;
using _3._Scripts.Extensions.CustomDictionaries;
using FSG.MeshAnimator;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Units.Scriptable.Animations
{
    [CreateAssetMenu(menuName = "Configs/Units/Animations", fileName = "MeshAnimationsHolder")]
    public class MeshAnimationsHolder : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, List<MeshAnimationBase>,
            MeshAnimationListStorage> animations;

        public MeshAnimationBase Get(string key, int id = 0)
        {
            return animations[key][id];
        }

        public IEnumerable<MeshAnimationBase> GetList(string key)
        {
            return animations[key];
        }
    }
}