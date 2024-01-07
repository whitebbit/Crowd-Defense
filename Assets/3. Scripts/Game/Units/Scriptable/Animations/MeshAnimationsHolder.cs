using System.Collections.Generic;
using _3._Scripts.Extensions;
using FSG.MeshAnimator;
using FSG.MeshAnimator.Snapshot;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Game.Units.Scriptable.Animations
{
    [CreateAssetMenu(menuName = "Configs/Units/Animations", fileName = "MeshAnimationsHolder")]
    public class MeshAnimationsHolder: ScriptableObject
    {
        [SerializeField] private KeyValueHolder<List<MeshAnimationBase>> animations;
        
        public MeshAnimationBase Get(string key, int id = 0)
        {
            return animations.GetValue(key)[id];
        }
        public IEnumerable<MeshAnimationBase> GetList(string key)
        {
            return animations.GetValue(key);
        }
    }
}