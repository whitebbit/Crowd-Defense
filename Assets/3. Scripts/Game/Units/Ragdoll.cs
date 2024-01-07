using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.Game.Units
{
    public class Ragdoll
    {
        public List<Collider> Colliders { get; private set; }= new List<Collider>();
        public List<Rigidbody> Rigidbodies { get; private set; }= new List<Rigidbody>();

        public Ragdoll(Component body)
        {
            Initialize(body);
            CollidersState(true);
            RigidbodiesState(false);
        }
    
        public void CollidersState(bool state)
        {
            foreach (var collider in Colliders)
            {
                collider.enabled = state;
            }
        }
        public void RigidbodiesState(bool state)
        {
            foreach (var rigidbody in Rigidbodies)
            {
                rigidbody.isKinematic = !state;
            }
        }

        private void Initialize(Component body)
        {
            Colliders = new List<Collider>(body.GetComponentsInChildren<Collider>());
            Rigidbodies = new List<Rigidbody>(body.GetComponentsInChildren<Rigidbody>());
        }
    }
}