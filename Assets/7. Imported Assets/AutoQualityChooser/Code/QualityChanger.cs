using UnityEngine;

namespace net.krej.AutoQualityChooser
{
    public class QualityChanger
    {
        private int currQuality = -1;
        public string currentQuality;

        public void IncreaseQuality() {
            AddToQuality(1);
        }

        public void DecreaseQuality() {
            AddToQuality(-1);
        }

        private void AddToQuality(int amount) {
            if (AutoQualityChooser.Instance.settings.disableAfterManualQualityChange && currQuality >= 0 && currQuality != QualitySettings.GetQualityLevel()) {
                // After manual quality change
                AutoQualityChooser.Instance.enabled = false;
                return;
            }
            currQuality = QualitySettings.GetQualityLevel();
            SetQuality(currQuality + amount);
        }

        public void SetQuality(int value) {
            if (value < 0) return;
            if (value >= QualitySettings.names.Length) value = QualitySettings.names.Length - 1;
            currQuality = QualitySettings.GetQualityLevel();
            if (value != currQuality) {
                QualitySettings.SetQualityLevel(value, false);
                currQuality = QualitySettings.GetQualityLevel();
                currentQuality = "" + currQuality + " (" + QualitySettings.names[currQuality] + ")";
                if (AutoQualityChooser.Instance.onQualityChange != null) AutoQualityChooser.Instance.onQualityChange.Invoke();
            }
            else {
                currentQuality = "" + currQuality + " (" + QualitySettings.names[currQuality] + ")";
            }
        }

        public static string GetCurrentQualityName(){
            var currQuality = QualitySettings.GetQualityLevel();
            var currentQuality = string.Format("{0}/{1}: {2}", currQuality+1, QualitySettings.names.Length, QualitySettings.names[currQuality]);
            return currentQuality;
        }
    }
}