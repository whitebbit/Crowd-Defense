using System.Collections.Generic;
using _3._Scripts.Architecture;
using _3._Scripts.Game.Units;
using _3._Scripts.Game.Weapon;
using _3._Scripts.Game.Weapon.Scriptable;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Main
{
    public class Player: Singleton<Player>
    {
        [SerializeField] private List<WeaponBehaviour> weapons = new();
        private WeaponBehaviour _currentWeapon;

        protected void Start()
        {
            SelectWeapon(YandexGame.savesData.currentWeapon);
        }

        public void SelectWeapon(string id)
        {
            if(_currentWeapon != null)
                _currentWeapon.SetState(false);

            _currentWeapon = weapons.Find(w => w.ID == id);
            
            _currentWeapon.SetState(true);
        }
    }
}