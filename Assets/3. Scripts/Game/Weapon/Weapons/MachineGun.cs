using _3._Scripts.FSM.Base;
using _3._Scripts.FSM.Interfaces;
using _3._Scripts.Game.Weapon.Scriptable;
using _3._Scripts.Game.Weapon.Types;
using UnityEngine;

namespace _3._Scripts.Game.Weapon.Weapons
{
    public class MachineGun : Firearms
    {

       
        public MachineGun(FirearmsConfig config) : base(config)
        {
            AttackCondition = new FuncPredicate(
                () => Input.GetMouseButton(0) && CanShoot());
            ReloadCondition = new FuncPredicate(NoBullets);
        }

        protected override void OnUpdate()
        {
            
        }
        
        protected override void OnAttack()
        {
            ChangeBulletCount(-1);
            ResetAttackTime();
            
            Debug.Log("Shoot");
        }

        protected override void OnReload()
        {
            ResetBulletCount();
            Debug.Log("Reload");
        }
    }
}