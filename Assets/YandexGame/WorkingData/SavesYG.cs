
using System.Collections.Generic;
using UnityEngine.Serialization;

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


        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
    }
}
