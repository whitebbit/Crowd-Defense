using UnityEngine;
using UnityEngine.Events;

namespace net.krej.FPSCounter {
    public class FramerateCounter : net.krej.Singleton.Singleton<FramerateCounter>{
        public float currentFrameRate;
        public UnityEvent onFramerateCalculated = new UnityEvent();
        private float updateRate = 1.0f;
        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval

        private void Update(){
            timeleft -= Time.deltaTime;
            accum += Time.timeScale/Time.deltaTime;
            ++frames;
            if (timeleft <= 0.0) StartNewInterval();
        }

        private void StartNewInterval(){
            currentFrameRate = accum/frames;
            ResetTimeLeft();
            accum = 0.0F;
            frames = 0;
            onFramerateCalculated.Invoke();
        }

        public void ResetTimeLeft(){
            timeleft = 1.0f/updateRate;
        }
    }
}