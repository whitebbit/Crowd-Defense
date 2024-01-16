
using System;
using System.Collections.Generic;
using _3._Scripts.Game;
using UnityEngine.Serialization;
using Random =UnityEngine.Random;
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;


        // Ваши сохранения
        public int money;                       
        public int health = 100;  
        
        public string currentWeapon = "machine_gun";
        public string secondWeapon = "";
        public Dictionary<string, int> weaponsLevel = new ();
        public List<string> unlockedWeapons = new ()
        {
            "machine_gun",
        };
        
        public int currentLevel = 1;
        public int completedLevelsCount;


        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        public int GetWeaponLevel(string id)
        {
            return weaponsLevel.TryGetValue(id, out var level) ? level : 0;
        }

        
        
        // Вы можете выполнить какие то действия при загрузке сохранений
    }
}
