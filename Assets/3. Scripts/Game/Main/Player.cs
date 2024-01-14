﻿using System;
using System.Collections.Generic;
using _3._Scripts.Architecture;
using _3._Scripts.Game.Units;
using _3._Scripts.Game.Weapon;
using _3._Scripts.Game.Weapon.Scriptable;
using Cinemachine;
using UnityEngine;
using YG;

namespace _3._Scripts.Game.Main
{
    public class Player : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private CinemachineVirtualCamera camera;
        [Header("Weapons")]
        [SerializeField] private List<WeaponBehaviour> weapons = new();
        
        private WeaponBehaviour _currentWeapon;

        protected void Awake()
        {
            foreach (var weapon in weapons)
            {
                weapon.SetState(false);
            }
        }

        public void SelectWeapon(string id)
        {
            if (_currentWeapon != null)
                _currentWeapon.SetState(false);

            _currentWeapon = weapons.Find(w => w.ID == id);
            
            _currentWeapon.SetState(true);
        }

        public void SetCameraState(bool state)
        {
            camera.Priority = state ? 100 : -1;
        }
        
        public WeaponBehaviour GetWeapon(string id)
        {
            return weapons.Find(w => w.ID == id);
        }
    }
}