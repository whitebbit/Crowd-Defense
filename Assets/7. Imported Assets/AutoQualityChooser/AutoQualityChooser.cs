using UnityEngine;
using UnityEngine.Events;
using net.krej.FPSCounter;

namespace net.krej.AutoQualityChooser {
    [RequireComponent(typeof(FramerateCounter))]
    public class AutoQualityChooser : net.krej.Singleton.Singleton<AutoQualityChooser> {
        public UnityEvent onQualityChange;
        public AutoQualityChooserSettings settings = new AutoQualityChooserSettings();
        private FramerateCounter framerateCounter;

        public int secondsBeforeDecreasingQuality = 5;
        private readonly QualityChanger qualityChanger = new QualityChanger();

        void Awake() {
            if(AreTooManyOnScene())Destroy(gameObject);
        }

        void Start() {
            framerateCounter = GetComponent<FramerateCounter>();
            SetProperStartQuality();
            framerateCounter.ResetTimeLeft();
            framerateCounter.onFramerateCalculated.AddListener(OnFramerateUpdated);
            ResetQualityDowngradeTimer();
        }

        private void SetProperStartQuality() {
            var startQuality = settings.startQuality;
            #if UNITY_EDITOR
            startQuality = settings.startQualityInUnityEditor;
            #endif
            if (startQuality == -1) startQuality = QualitySettings.names.Length - 1;
            qualityChanger.SetQuality(startQuality);
        }

        private void ResetQualityDowngradeTimer(){
            secondsBeforeDecreasingQuality = settings.timeBeforeQualityDowngrade;
        }

        private void OnFramerateUpdated() {
            if(!enabled)return;
            if (IsFramerateTooLow()) secondsBeforeDecreasingQuality--;
            else ResetQualityDowngradeTimer();
            if (secondsBeforeDecreasingQuality < 0) DecreaseQuality();
        }

        private bool IsFramerateTooLow(){
            return framerateCounter.currentFrameRate < settings.minAcceptableFramerate;
        }

        private void DecreaseQuality(){
            qualityChanger.DecreaseQuality();
            ResetQualityDowngradeTimer();
        }
    }
}
