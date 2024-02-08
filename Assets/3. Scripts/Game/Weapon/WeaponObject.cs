using _3._Scripts.Game.Weapon.Animation;
using UnityEngine;

namespace _3._Scripts.Game.Weapon
{
    public class WeaponObject : MonoBehaviour
    {
        [SerializeField] private Transform point;
        [Space]
        [SerializeField] private Transform decalPoint;
        [SerializeField] private ParticleSystem explosion;
        [Space]
        [SerializeField] private WeaponAnimator animator;

        [Space] [SerializeField] private string gunshotSoundId;
        public Transform Point => point;

        public void SetState(bool state) => gameObject.SetActive(state);

        public void SpawnDecals()
        {
            Instantiate(explosion, decalPoint.position, decalPoint.rotation, decalPoint);
        }

        public void PlayGunshotSound()
        {
            AudioManager.Instance.PlayOneShot(gunshotSoundId);
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