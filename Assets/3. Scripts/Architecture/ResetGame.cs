using UnityEditor;
using UnityEngine;
using YG;

namespace _3._Scripts.Architecture
{
#if UNITY_EDITOR
    public class ResetGame: Editor
    {
        [MenuItem("Tools/Reset Game")]
        private static void Resenting()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
        
        [MenuItem("Tools/Time Scale/Default")]
        private static void TimeScaleDefault()
        {
            Time.timeScale = 1;
        }
        
        [MenuItem("Tools/Time Scale/Slowmotion")]
        private static void TimeScaleSlowmotion()
        {
            Time.timeScale = 0.25f;
        }
    }
#endif
}