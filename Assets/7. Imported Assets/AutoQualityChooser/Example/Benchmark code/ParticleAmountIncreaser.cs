using UnityEngine;
using net.krej.AutoQualityChooser;
using net.krej.FPSCounter;

public class ParticleAmountIncreaser : MonoBehaviour {
    private ParticleSystem particles;

    void Start() {
        particles = GetComponent<ParticleSystem>();
        SetEmissionRate(0);

    }

    void Update() {
        if (FramerateCounter.Instance.currentFrameRate >= 0.9 * AutoQualityChooser.Instance.settings.minAcceptableFramerate)
            SetEmissionRate(GetEmissionRate() + Time.deltaTime * 10);
    }

    void SetEmissionRate(float value) {
        #if UNITY_5_3_OR_NEWER
        var emission = particles.emission;
        var rate = emission.rate;
        rate.constantMax = value;
        emission.rate = rate;
        #else
        particles.emissionRate = value;
        #endif
    }

    float GetEmissionRate() {
        #if UNITY_5_3_OR_NEWER
        return particles.emission.rate.constantMax;
        #else
        return particles.emissionRate;
        #endif
    }
}
