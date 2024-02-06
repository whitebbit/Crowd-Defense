using System;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI.Components
{
    public class FpsCounter: MonoBehaviour
    {
        public float currentFrameRate;

        private const float UpdateRate = 1.0f;
        private float _accum = 0; // FPS accumulated over the interval
        private int _frames = 0; // Frames drawn over the interval
        private float _timeLeft; // Left time for current interval
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update(){
            _timeLeft -= Time.deltaTime;
            _accum += Time.timeScale/Time.deltaTime;
            ++_frames;
            if (_timeLeft <= 0.0) StartNewInterval();

            _text.text = currentFrameRate.ToString();
        }

        private void StartNewInterval(){
            currentFrameRate = _accum/_frames;
            ResetTimeLeft();
            _accum = 0.0F;
            _frames = 0;
        }
        private void ResetTimeLeft(){

            _timeLeft = 1.0f/UpdateRate;
        }
    }
}