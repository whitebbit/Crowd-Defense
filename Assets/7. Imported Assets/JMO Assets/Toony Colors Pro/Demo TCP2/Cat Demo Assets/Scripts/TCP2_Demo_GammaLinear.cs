// Toony Colors Pro+Mobile 2
// (c) 2014-2020 Jean Moreno

// This script handles lights' and ambient intensities depending on if the Editor is set to Gamma or Linear color space

using System;
using UnityEngine;

namespace ToonyColorsPro
{
    namespace Demo
    {
        [ExecuteInEditMode]
        public class TCP2_Demo_GammaLinear : MonoBehaviour
        {
            public LightSettings[] lights;
            public MaterialSettings[] materials;
            private ColorSpace lastColorSpace;

            [Serializable]
            public class LightSettings
            {
                public Light light;
                public float gammaIntensity;
                public float linearIntensity;
            }

            [Serializable]
            public class MaterialSettings
            {
                public Material material;
                public Color gammaColor;
                public Color linearColor;
            }

#if UNITY_EDITOR
            private void Awake()
            {
                lastColorSpace = QualitySettings.activeColorSpace;
                UpdateLighting();
            }

            private void Update()
            {
                if (lastColorSpace != QualitySettings.activeColorSpace)
                {
                    lastColorSpace = QualitySettings.activeColorSpace;
                    UpdateLighting();
                }
            }

            private void UpdateLighting()
            {
                var isLinear = QualitySettings.activeColorSpace == ColorSpace.Linear;

                if (lights != null)
                    foreach (var ls in lights)
                        ls.light.intensity = isLinear ? ls.linearIntensity : ls.gammaIntensity;

                if (lights != null)
                    foreach (var mat in materials)
                        mat.material.color = isLinear ? mat.linearColor : mat.gammaColor;
            }
#endif
        }
    }
}