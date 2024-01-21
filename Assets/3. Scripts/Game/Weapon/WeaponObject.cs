using _3._Scripts.Game.Weapon.Animation;
using UnityEngine;

namespace _3._Scripts.Game.Weapon
{
    public class WeaponObject : MonoBehaviour
    {
        [SerializeField] private Transform point;
        [SerializeField] private Transform decalPoint;
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private WeaponAnimator animator;
        public Transform Point => point;

        public void SetState(bool state) => gameObject.SetActive(state);

        public void SpawnDecals()
        {
            Instantiate(explosion, decalPoint.position, decalPoint.rotation, decalPoint);
        }

        public void AnimatorState(bool state)
        {
            if (animator == null) return;

            if (state)
                animator.DoAnimation();
            else
                animator.Stop();
        }
    }
}